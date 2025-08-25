using HR_System.DTOs;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Helpers;
using HR_System.Interfaces.Repository;
using HR_System.Interfaces.Service;
using HR_System.JwtAuth;
using Mapster;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Service;
public class UserService(IUserRepository userRepository, JwtService jwtService, 
    IUnitOfWork unitOfWork,
    IEmployerRepository employerRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployerRepository _employerRepository = employerRepository;

    public string GeneratePasswordForUser()
    {
        return PasswordHelper.PasswordGeneration();
    }

    public async Task<UserDto?> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
        {
            throw new ApiException("User or already exists.");
        }
        if (await _userRepository.ExistsAsync(userRegisterDto.Email))
        {
            throw new ApiException("Email or already exists.");
        } 
        if (!Enum.IsDefined(userRegisterDto.Role))
        {
            throw new ApiException("Invalid role value.");
        }

        string password = GeneratePasswordForUser();

        var userId = await _userRepository.CreateAsync(new User
        {
            Username = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = userRegisterDto.Role
        });

        UserDto? employer = await _employerRepository.CreateAsync(userId, userRegisterDto) ?? throw new ApiException("Employer creation failed.");

        employer.Username = userRegisterDto.Username;
        employer.Email = userRegisterDto.Email;
        employer.Password = password;
        employer.Role = userRegisterDto.Role;

        await _unitOfWork.SaveChangesAsync(cancellationToken: CancellationToken.None);
        return employer;
    }

    public async Task<string?> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(userLoginDto.Username);
        if (user == null)
            return null;
        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            return null;
        return jwtService.GenerateToken(user);
    }

    public async Task<UserProfileDto?> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ApiException("Username cannot be null.");

        var user = await _userRepository.GetByUsernameAsync(username, includeEmployeeProfile: true);

        if (user == null)
            return null;

        var employeeProfile = user.Adapt<UserProfileDto>();

        return employeeProfile;
    }

    public async Task<UserProfileDto?> AssignRoleAsync(string? username, UserRole role)
    {
        if (!Enum.IsDefined(typeof(UserRole), role))
            throw new Exception("Invalid role.");

        var user = await _userRepository.GetByUsernameAsync(username, includeEmployeeProfile: true);

        if (user == null)
            return null;

        user.Role = role;
        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserProfileDto>();
    }
}

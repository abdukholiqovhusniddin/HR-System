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
    IFileService fileService,
    IEmailService emailService,
    IEmployerRepository employerRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployerRepository _employerRepository = employerRepository;

    public string GeneratePasswordForUser() =>
        PasswordHelper.PasswordGeneration();
    
    public async Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
            throw new ApiException("User already exists.");
        if (await _userRepository.ExistsAsync(userRegisterDto.Email))
            throw new ApiException("Email already exists.");
        if (!Enum.IsDefined(userRegisterDto.Role))
            throw new ApiException("Invalid role value.");

        string password = GeneratePasswordForUser();

        var user = new User
        {
            Username = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = userRegisterDto.Role
        };

        var userId = await _userRepository.CreateAsync(user);

        // создаём сущность Employer
        var newEmployer = new Employee
        {
            FullName = userRegisterDto.FullName,
            DateOfBirth = userRegisterDto.DateOfBirth,
            Email = userRegisterDto.Email,
            IsEmailPublic = userRegisterDto.IsEmailPublic,
            PhoneNumber = userRegisterDto.PhoneNumber,
            Telegram = userRegisterDto.Telegram,
            IsTelegramPublic = userRegisterDto.IsTelegramPublic,
            Position = userRegisterDto.Position,
            Department = userRegisterDto.Department,
            HireDate = userRegisterDto.HireDate,
            PassportInfo = userRegisterDto.PassportInfo,
            UserId = userId
        };

        // фото загружаем через сервис (Application уровень)
        if (userRegisterDto.Photo is not null)
        {
            var imagePath = await fileService.SaveAsync(userRegisterDto.Photo);

            newEmployer.Image = new DataFile(newEmployer.Id,
                imagePath.Name,
                imagePath.Url,
                imagePath.Size,
                imagePath.Extension);

            newEmployer.PhotoUrl = imagePath.Url;
        }


        var userDto = await _employerRepository.CreateAsync(newEmployer)
            ?? throw new ApiException("Failed to create user profile.");

        userDto.Password = password;
        userDto.Username = userRegisterDto.Username;
        userDto.Email = userRegisterDto.Email;
        userDto.Role = userRegisterDto.Role;

        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        await emailService.SendPasswordEmailGmailAsync(userRegisterDto.Email, userRegisterDto.Username, password);
        return userDto;
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

        return user is null ? throw new ApiException("User not found.")
            : user.Adapt<UserProfileDto>();
    }

    public async Task<UserProfileDto?> AssignRoleAsync(AssignRoleDto dto)
    {
        if (!Enum.IsDefined(typeof(UserRole), dto.Role))
            throw new Exception("Invalid role.");

        var user = await _userRepository.GetByUsernameAsync(dto.Username, includeEmployeeProfile: true);

        if (user == null)
            return null;

        user.Role = dto.Role;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserProfileDto>();
    }
}

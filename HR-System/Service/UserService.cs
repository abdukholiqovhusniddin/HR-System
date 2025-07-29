using HR_System.DTOs;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces;
using HR_System.JwtAuth;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Service;
public class UserService(IUserRepository userRepository, JwtService jwtService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto)
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

        await _userRepository.CreateAsync(new User
        {
            Username = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
            Role = userRegisterDto.Role
        });
        return new UserDto(userRegisterDto.Username, userRegisterDto.Email, userRegisterDto.Role);
    }

    public async Task<UserProfileDto?> GetByUsernameAsync(string username, string role, Guid userid)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
            return null;

        var userByRole = await _userRepository.GetUserInfoByRoleAsync(userid, role);

        UserProfileDto userProfile = new()
        {
            Username = userByRole?.Username,
            Email = userByRole?.Email,
            FullName = userByRole?.EmployeeProfile?.FullName,
        };

        return userProfile;
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
}

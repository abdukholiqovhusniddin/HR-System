using HR_System.Entities;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces.Service;
public interface IUserService
{
    Task<UserProfileDto?> AssignRoleAsync(string? username, UserRole role);
    Task<UserDto?> CreateUserAsync(UserRegisterDto userRegisterDto);
    Task<UserProfileDto?> GetByUsernameAsync(string username);
    Task<string?> LoginAsync(UserLoginDto userLoginDto);
}

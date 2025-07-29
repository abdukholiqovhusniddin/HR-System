using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces;
public interface IUserService
{
    Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto);
    Task<UserProfileDto?> GetByUsernameAsync(string username, string role, Guid userid);
    Task<string?> LoginAsync(UserLoginDto userLoginDto);
}

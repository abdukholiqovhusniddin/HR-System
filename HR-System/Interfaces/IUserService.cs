using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces;
public interface IUserService
{
    Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto);
    Task<object?> GetByUsernameAsync(string username, string role);
    Task<string?> LoginAsync(UserLoginDto userLoginDto);
}

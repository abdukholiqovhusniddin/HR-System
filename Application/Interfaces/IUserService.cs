namespace Application.Interfaces;
public interface IUserService
{
    Task<UserProfileDto?> AssignRoleAsync(AssignRoleDto dto);
    Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto);
    Task<UserProfileDto?> GetByUsernameAsync(string username);
    Task<string?> LoginAsync(UserLoginDto userLoginDto);
}

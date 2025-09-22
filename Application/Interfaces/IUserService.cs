using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;

namespace Application.Interfaces;
public interface IUserService
{
    Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto);
    Task<UserResponseDto> CreateUserAsync(UserRegisterRequestDto userRegisterDto);
    Task<UserProfileResponseDto?> GetByUsernameAsync(string username);
    Task<string?> LoginAsync(UserLoginRequestDto userLoginDto);
}

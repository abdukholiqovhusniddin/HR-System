using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;

namespace Application.Interfaces;
public interface IUserService
{
    Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto);
    Task<UserResponseDto> CreateUserAsync(UserRegisterRequestDto userRegisterDto);
    Task<UserProfileResponseDto?> GetByUsernameAsync(string username);
    Task<string?> LoginAsync(UserLoginRequestDto userLoginDto);
}

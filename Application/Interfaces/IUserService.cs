using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Interfaces;
public interface IUserService
{
    Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto);
    Task<UserResponseDto> CreateUserAsync(UserRegisterRequestDto userRegisterDto);
    Task<UserProfileResponseDto?> GetByUsernameAsync(string username);
    Task<string?> LoginAsync(UserLoginRequestDto userLoginDto);
}

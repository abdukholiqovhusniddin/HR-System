using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;
namespace Application.Interfaces;
public interface IUserService
{
    Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto);
    Task<UserProfileResponseDto?> GetByUsernameAsync(string username);
}

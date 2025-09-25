using Application.DTOs.Employees.Requests;
using Application.DTOs.Users.Responses;
namespace Application.Interfaces;
public interface IUserService
{
    Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto);
}

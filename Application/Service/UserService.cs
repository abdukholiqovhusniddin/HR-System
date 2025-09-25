using Application.Commons;
using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;
using Application.Exceptions;
using Application.Interfaces;
using Application.JwtAuth;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Mapster;

namespace Application.Service;
public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserProfileResponseDto?> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ApiException("Username cannot be null.");

        var user = await _userRepository.GetByUsernameAsync(username, includeEmployeeProfile: true);

       return user?.EmployeeProfile?.Adapt<UserProfileResponseDto>();
    }

    public async Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto)
    {
        if (!Enum.IsDefined(dto.Role))
            throw new Exception("Invalid role.");

        var user = await _userRepository.GetByUsernameAsync(dto.Username, includeEmployeeProfile: true);

        if (user == null)
            return null;

        user.Role = dto.Role;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserProfileResponseDto>();
    }
}

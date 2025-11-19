using Application.Commons;
using Application.DTOs.Users.Responses;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Users.Handlers;
internal sealed class AssignRoleCommandHandler(IUserRepository userRepository) : IRequestHandler<AssignRoleCommand, ApiResponse<UserProfileResponseDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<ApiResponse<UserProfileResponseDto>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.AssignRoleDto;
        var user = await _userRepository.GetByUsernameAsync(dto.Username, cancellationToken, includeEmployeeProfile: true)
            ?? throw new NotFoundException("User not found");

        user.Role = dto.Role;

        await _userRepository.UpdateAsync(user);

        var userDto = user.EmployeeProfile?.Adapt<UserProfileResponseDto>();

        return new ApiResponse<UserProfileResponseDto>(userDto!);
    }
}

using Application.Commons;
using Application.DTOs.Users.Responses;
using Application.Exceptions;
using Application.Features.Users.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Users.Handlers;

internal sealed class GetMeQueryHandler(IUserRepository userRepository) : IRequestHandler<GetMeQuery, ApiResponse<UserProfileResponseDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<ApiResponse<UserProfileResponseDto>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var userProfile = request.UserName;

        if (string.IsNullOrWhiteSpace(userProfile))
            throw new ApiException("Username cannot be null.");

        var user = await _userRepository.GetByUsernameAsync(userProfile, cancellationToken, includeEmployeeProfile: true)
            ?? throw new NotFoundException("User not found");

        var userDto = user.EmployeeProfile?.Adapt<UserProfileResponseDto>();

        return new ApiResponse<UserProfileResponseDto>(userDto!);

    }
}
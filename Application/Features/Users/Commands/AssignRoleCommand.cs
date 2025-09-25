using Application.Commons;
using Application.DTOs.Users.Responses;
using MediatR;

namespace Application.Features.Users.Commands;
public sealed record AssignRoleCommand: IRequest<ApiResponse<UserProfileResponseDto>>;
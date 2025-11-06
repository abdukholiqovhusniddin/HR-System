using Application.Commons;
using Application.DTOs.Users.Requests;
using MediatR;

namespace Application.Features.Users.Commands;
public sealed record SendResetPasswordCommand(SendResetPasswordRequestDto requestDto)
    : IRequest<ApiResponse<string>>;

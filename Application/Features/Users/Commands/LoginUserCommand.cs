using Application.Commons;
using Application.DTOs.Users.Requests;
using MediatR;

namespace Application.Features.Users.Commands;
public sealed record LoginUserCommand(UserLoginRequestDto UserLoginDto) : IRequest<ApiResponse<string>>;

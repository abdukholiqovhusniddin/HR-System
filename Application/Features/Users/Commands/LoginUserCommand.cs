using Application.DTOs.Employees.Requests;

namespace Application.Features.Users.Commands;
public sealed record LoginUserCommand(UserLoginRequestDto UserLoginDto) : IRequest<ApiResponse<string>>;

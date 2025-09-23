using Application.Commons;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;
using MediatR;

namespace Application.Features.Users.Commands;
public sealed record CreateUserCommand(UserRegisterRequestDto userRegisterDto) : IRequest<ApiResponse<UserResponseDto>>;
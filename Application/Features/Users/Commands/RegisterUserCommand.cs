using Application.Commons;
using Application.DTOs.Users.Requests;
using Application.DTOs.Users.Responses;
using MediatR;

namespace Application.Features.Users.Commands;
public sealed record RegisterUserCommand(UserRegisterRequestDto UserRegisterDto) 
    : IRequest<ApiResponse<UserResponseDto>>;
using Application.Commons;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Responses;
using MediatR;

namespace Application.Features.Users.Queries;

public sealed record GetMeQuery(string UserName) : IRequest<ApiResponse<UserProfileResponseDto>>;
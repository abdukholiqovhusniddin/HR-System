using Application.Commons;
using Application.DTOs.Employees.Responses;
using MediatR;

namespace Application.Features.Employees.Queries;
public sealed record GetEmployeeDirectory : IRequest<ApiResponse<List<ResponseDirectoryDto>>>;

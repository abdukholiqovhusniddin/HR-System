using Application.Commons;
using Application.DTOs.Employees.Responses;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Employees.Queries;

public sealed record GetEmployeeById(Guid EmployeeId) : IRequest<ApiResponse<ResponseEmployeeDto>>;

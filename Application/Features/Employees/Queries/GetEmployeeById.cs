using Application.Commons;
using Application.DTOs.Employees.Responses;
using MediatR;

namespace Application.Features.Employees.Queries;

public record GetEmployeeById(Guid EmployeeId) : IRequest<ApiResponse<ResponseEmployeeDto>>;
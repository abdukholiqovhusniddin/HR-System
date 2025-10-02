using Application.Commons;
using Application.DTOs.Salaries.Responses;
using MediatR;

namespace Application.Features.Salaries.Queries;
public record GetSalaryEmployeeQuery(Guid salaryEmployee) : IRequest<ApiResponse<SalaryEmployeeDtoResponse>>;

using Application.Commons;
using Application.DTOs.Salaries.Responses;
using MediatR;

namespace Application.Features.Salaries.Queries;
public record GetSalaryHistoryQuery(Guid salaryEmployee) : IRequest<ApiResponse<List<SalaryDtoResponse>>>;

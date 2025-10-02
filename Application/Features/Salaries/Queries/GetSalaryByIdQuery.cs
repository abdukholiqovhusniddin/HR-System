using Application.Commons;
using Application.DTOs.Salaries.Responses;
using MediatR;

namespace Application.Features.Salaries.Queries;
public record GetSalaryByIdQuery(Guid EmployeeId) : IRequest<ApiResponse<SalaryEmployeeAndHistoryDtoResponse>>;

using Application.Commons;
using MediatR;

namespace Application.Features.Salaries.Queries;
public record GetSalaryByIdQuery(Guid EmployeeId) : IRequest<ApiResponse<List<Salary>>>;

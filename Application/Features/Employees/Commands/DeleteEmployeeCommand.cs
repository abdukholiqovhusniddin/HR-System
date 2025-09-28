using Application.Commons;
using MediatR;

namespace Application.Features.Employees.Commands;
public sealed record DeleteEmployeeCommand(Guid Id) : IRequest<ApiResponse<Employee>>;
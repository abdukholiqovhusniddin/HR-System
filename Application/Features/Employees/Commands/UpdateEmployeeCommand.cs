using Application.Commons;
using Application.DTOs.Employees.Requests;
using MediatR;

namespace Application.Features.Employees.Commands;
public sealed record UpdateEmployeeCommand(UpdateEmployeeDtoRequest UpdateEmployee) 
    : IRequest<ApiResponse<UpdateEmployeeDtoRequest>>;
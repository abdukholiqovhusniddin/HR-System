using Application.Commons;
using Application.DTOs.Salaries.Requests;
using MediatR;

namespace Application.Features.Salaries.Commands;
public record CreateSalaryCommand(AddSalaryDtoRequest salary): IRequest<ApiResponse<Salary>>;

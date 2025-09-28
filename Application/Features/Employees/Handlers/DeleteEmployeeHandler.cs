using Application.Commons;
using Application.Features.Employees.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Employees.Handlers;
public class DeleteEmployeeHandler(IEmployeesRepository employeesRepository):IRequestHandler<DeleteEmployeeCommand, ApiResponse<Employee>>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    public async Task<ApiResponse<Employee>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeesRepository.GetById(request.Id);
        if (employee is null)
        {
            return new ApiResponse<Employee>
            {
                StatusCode = 404,
                Error = "Employee not found"
            };
        }

        employee.IsActive = false;

        return new ApiResponse<Employee>
        {
            Data = employee,
            StatusCode = 200
        };
    }
}

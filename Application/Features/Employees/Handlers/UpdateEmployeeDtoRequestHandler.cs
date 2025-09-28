using Application.Commons;
using Application.DTOs.Employees.Requests;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Employees.Handlers;
public class UpdateEmployeeDtoRequestHandler(IEmployeesRepository employeesRepository,
    IUnitOfWork unitOfWork) : IRequest<ApiResponse<UpdateEmployeeDtoRequest>>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ApiResponse<Employee>> Handle(UpdateEmployeeDtoRequest request, CancellationToken cancellationToken)
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

        employee = request.Adapt(employee);

        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Employee>
        {
            Data = employee,
            StatusCode = 200
        };
    }
}

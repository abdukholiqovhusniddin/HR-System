using Application.Commons;
using Application.Exceptions;
using Application.Features.Employees.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Employees.Handlers;
public class UpdateEmployeeHandler(IEmployeesRepository employeesRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEmployeeCommand, ApiResponse<Employee>>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ApiResponse<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var updateDto = request.UpdateEmployee;
        var employee = await _employeesRepository.GetById(updateDto.Id)
            ?? throw new NotFoundException("Employee not found");

        employee = updateDto.Adapt(employee);

        await _employeesRepository.Update(employee);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Employee>
        {
            Data = employee,
        };
    }
}

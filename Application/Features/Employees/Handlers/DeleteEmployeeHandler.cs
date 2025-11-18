using Application.Commons;
using Application.Exceptions;
using Application.Features.Employees.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Employees.Handlers;
public class DeleteEmployeeHandler(IEmployeesRepository employeesRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteEmployeeCommand, ApiResponse<Unit>>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    public async Task<ApiResponse<Unit>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeesRepository.GetById(request.Id, cancellationToken, false)
            ?? throw new NotFoundException("Employee not found");

        employee.IsActive = false;

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Unit>();
    }
}
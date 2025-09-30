using Application.Commons;
using Application.DTOs.Employees.Responses;
using Application.Exceptions;
using Application.Features.Employees.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Employees.Handlers;

internal sealed class GetEmployeeByIdHandler(IEmployeesRepository directory) : IRequestHandler<GetEmployeeById, ApiResponse<ResponseEmployeeDto>>
{
    private readonly IEmployeesRepository _directoryRepository = directory;
    public async Task<ApiResponse<ResponseEmployeeDto>> Handle(GetEmployeeById request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == Guid.Empty)
            throw new ApiException("Id is empty");

        var employee = await _directoryRepository.GetById(request.EmployeeId);
        
        return employee is null
            ? throw new NotFoundException("Employee not found")
            : new ApiResponse<ResponseEmployeeDto>
        {
            Data = employee.Adapt<ResponseEmployeeDto>()
        };
    }
}
using Application.Commons;
using Application.DTOs.Salaries.Responses;
using Application.Exceptions;
using Application.Features.Salaries.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Salaries.Handlers;
public class GetSalaryEmployeeHandler(ISalariesRepository salariesRepository) 
    : IRequestHandler<GetSalaryEmployeeQuery, ApiResponse<SalaryEmployeeDtoResponse>>
{
    private readonly ISalariesRepository _salariesRepository = salariesRepository;
    public async Task<ApiResponse<SalaryEmployeeDtoResponse>> Handle(GetSalaryEmployeeQuery request, CancellationToken cancellationToken)
    {
        Guid salaryEmployee = request.salaryEmployee;
        if (salaryEmployee == Guid.Empty)
            throw new ApiException("The salary employee Id is required");

        var salary = await _salariesRepository.GetByEmployeeId(salaryEmployee)
            ?? throw new ApiException("The salary employee doesn't exist");
        return new ApiResponse<SalaryEmployeeDtoResponse>();
    }
}

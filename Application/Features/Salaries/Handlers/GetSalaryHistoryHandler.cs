using Application.Commons;
using Application.DTOs.Salaries.Responses;
using Application.Exceptions;
using Application.Features.Salaries.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Salaries.Handlers;
public class GetSalaryHistoryHandler(ISalariesRepository salariesRepository) 
    : IRequestHandler<GetSalaryHistoryQuery, ApiResponse<List<SalaryDtoResponse>>>
{
    private readonly ISalariesRepository _salariesRepository = salariesRepository;
    public async Task<ApiResponse<List<SalaryDtoResponse>>> Handle(GetSalaryHistoryQuery request, CancellationToken cancellationToken)
    {
        Guid salaryEmployee = request.salaryEmployee;
        if (salaryEmployee == Guid.Empty)
            throw new ApiException("The salary employee Id is required");

        var salary = await _salariesRepository.GetHistoryByEmployeeId(salaryEmployee)
            ?? throw new ApiException("The salary employee doesn't exist");

        var response = salary.Adapt<List<SalaryDtoResponse>>();

        if (response.Count == 0)
            throw new NotFoundException("Salary history not found");
        return new ApiResponse<List<SalaryDtoResponse>>(response);
    }
}

using Application.Commons;
using Application.DTOs.Salaries.Responses;
using Application.Exceptions;
using Application.Features.Salaries.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Salaries.Handlers;
public class GetSalaryByIdHandler(ISalariesRepository salariesRepository)
    : IRequestHandler<GetSalaryByIdQuery, ApiResponse<SalaryEmployeeAndHistoryDtoResponse>>
{
    private readonly ISalariesRepository _salariesRepository = salariesRepository;
    public async Task<ApiResponse<SalaryEmployeeAndHistoryDtoResponse>> Handle(GetSalaryByIdQuery request, CancellationToken cancellationToken)
    {
        Guid employeeId = request.EmployeeId;
        if (employeeId == Guid.Empty)
            throw new ApiException("Id is empty");

        var salary =  await _salariesRepository.GetByEmployeeId(employeeId);

        var response = salary.Adapt<SalaryEmployeeAndHistoryDtoResponse>();

        return salary is null
            ? throw new NotFoundException("Salary not found")
            : new ApiResponse<SalaryEmployeeAndHistoryDtoResponse>(response);
    }
}

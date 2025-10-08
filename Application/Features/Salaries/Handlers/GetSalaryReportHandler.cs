using Application.Commons;
using Application.DTOs.Salaries.Responses;
using Application.Exceptions;
using Application.Features.Salaries.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Salaries.Handlers;
public class GetSalaryReportHandler(ISalariesRepository salariesRepository) :
    IRequestHandler<GetSalaryReportQuery, ApiResponse<List<SalaryDtoResponse>>>
{
    private readonly ISalariesRepository _salariesRepository = salariesRepository;

    public async Task<ApiResponse<List<SalaryDtoResponse>>> Handle(GetSalaryReportQuery request, CancellationToken cancellationToken)
    {
        var salaries = await _salariesRepository.GetSalaryReport();
        if (salaries == null || salaries.Count == 0)
        {
            throw new ApiException("No salary data available for the report.");
        }

        var result = salaries.Adapt<List<SalaryDtoResponse>>();

        return new ApiResponse<List<SalaryDtoResponse>>(result);
    }
}
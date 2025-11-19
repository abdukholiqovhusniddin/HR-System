using Application.Commons;
using Application.DTOs.Salaries.Responses;
using Application.Exceptions;
using Application.Features.Salaries.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Salaries.Handlers;
public class CreateSalaryHandler(ISalariesRepository salariesRepository)
    : IRequestHandler<CreateSalaryCommand, ApiResponse<SalaryDtoResponse>>
{
    private readonly ISalariesRepository _salariesRepository = salariesRepository;

    public async Task<ApiResponse<SalaryDtoResponse>> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
    {
        var salary = request.salary;
        if (await _salariesRepository.IsEmployeeAktive(salary.EmployeeId, cancellationToken))
            throw new NotFoundException("Employee not found");
        var newSalary = salary.Adapt<Salary>();

        await _salariesRepository.CreateAsync(newSalary);

        var response = newSalary.Adapt<SalaryDtoResponse>();

        return new ApiResponse<SalaryDtoResponse>
        {
            Data = response,
            StatusCode = 201
        };

    }
}
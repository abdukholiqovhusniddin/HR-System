using Application.Commons;
using Application.Features.Salaries.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Salaries.Handlers;
public class CreateSalaryHandler(ISalariesRepository salariesRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSalaryCommand, ApiResponse<Salary>>
{
    private readonly ISalariesRepository _salariesRepository = salariesRepository;

    public async Task<ApiResponse<Salary>> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
    {
        var salary = request.salary;
        var newSalary = salary.Adapt<Salary>();
        
        await _salariesRepository.CreateAsync(newSalary);

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Salary>
        {
            Data = newSalary,
            StatusCode = 201
        };

    }
}

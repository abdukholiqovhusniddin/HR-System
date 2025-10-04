using Application.Commons;
using Application.Features.Vacations.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class CreateVacationHandler(IVacationRepository vacationRepository) 
    : IRequestHandler<CreateVacationCommand, ApiResponse<Vacation>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public async Task<ApiResponse<Vacation>> Handle(CreateVacationCommand request, CancellationToken cancellationToken)
    {
        Guid userId = request.UserId;
        var createVacationDtoRequest = request.CreateVacationDtoRequest;

        var vacation = createVacationDtoRequest.Adapt<Vacation>();
        userId = await _vacationRepository.GetEmployeeIdByUserIdAsync(userId);
        vacation.EmployeeId = userId;

        await _vacationRepository.CreateVacationAsync(userId, vacation);

        return new ApiResponse<Vacation>(vacation);

    }
}

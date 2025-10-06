using Application.Commons;
using Application.DTOs.Vacations.Responses;
using Application.Features.Vacations.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class CreateVacationHandler(IVacationRepository vacationRepository) 
    : IRequestHandler<CreateVacationCommand, ApiResponse<VacationDtoResponse>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public async Task<ApiResponse<VacationDtoResponse>> Handle(CreateVacationCommand request, CancellationToken cancellationToken)
    {
        Guid userId = request.UserId;
        var createVacationDtoRequest = request.CreateVacationDtoRequest;

        var vacation = createVacationDtoRequest.Adapt<Vacation>();
        userId = await _vacationRepository.GetEmployeeIdByUserIdAsync(userId);
        vacation.EmployeeId = userId;

        await _vacationRepository.CreateVacationAsync(userId, vacation);

        var response = vacation.Adapt<VacationDtoResponse>();

        return new ApiResponse<VacationDtoResponse>(response);

    }
}

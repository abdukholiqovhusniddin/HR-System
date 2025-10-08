using Application.Commons;
using Application.DTOs.Vacations.Responses;
using Application.Exceptions;
using Application.Features.Vacations.Commands;
using Domain.Enums;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class RejectVacationHandler(IVacationRepository vacationRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<RejectVacationCommand, ApiResponse<VacationDtoResponse>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public async Task<ApiResponse<VacationDtoResponse>> Handle(RejectVacationCommand request, CancellationToken cancellationToken)
    {
        var vacationId = request.VacationId;
        var vacation = await _vacationRepository.ApproveAndRejectVacation(vacationId)
            ?? throw new ApiException("Vacation not found or cannot be rejected");

        vacation.Status = VacationStatus.Rejected;

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        var response = vacation.Adapt<VacationDtoResponse>();
        return new ApiResponse<VacationDtoResponse>(response);
    }
}
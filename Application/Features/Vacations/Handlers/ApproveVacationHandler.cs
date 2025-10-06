using Application.Commons;
using Application.DTOs.Vacations.Responses;
using Application.Exceptions;
using Application.Features.Vacations.Commands;
using Domain.Enums;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class ApproveVacationHandler(IVacationRepository vacationRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<ApproveVacationCommand, ApiResponse<VacationDtoResponse>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public async Task<ApiResponse<VacationDtoResponse>> Handle(ApproveVacationCommand request, CancellationToken cancellationToken)
    {
        Guid vacationId = request.VacationId;
        if (vacationId == Guid.Empty)
            throw new ApiException("Invalid vacation ID");

        var vacation = await _vacationRepository.ApproveAndRejectVacation(vacationId)
            ?? throw new ApiException("Vacation not found or could not be approved");

        vacation.Status = VacationStatus.Approved;

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        var response = vacation.Adapt<VacationDtoResponse>();
        return new ApiResponse<VacationDtoResponse>(response);
    }
}

using Application.Commons;
using Application.DTOs.Vacations.Responses;
using Application.Features.Vacations.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class GetPendingVacationsHandler(IVacationRepository vacationRepository) : IRequestHandler<GetPendingVacationsQuery, ApiResponse<List<VacationDtoResponse>>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public async Task<ApiResponse<List<VacationDtoResponse>>> Handle(GetPendingVacationsQuery request, CancellationToken cancellationToken)
    {
        var vacations = await _vacationRepository.GetPendingVacationsAsync();

        var response = vacations.Adapt<List<VacationDtoResponse>>();
        return new ApiResponse<List<VacationDtoResponse>>(response);
    }
}
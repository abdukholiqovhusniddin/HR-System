using Application.Commons;
using Application.DTOs.Vacations.Responses;
using Application.Features.Vacations.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class GetMyVacationsHandler(IVacationRepository vacationRepository) : IRequestHandler<GetMyVacationsQuery, ApiResponse<List<VacationDtoResponse>>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public Task<ApiResponse<List<VacationDtoResponse>>> Handle(GetMyVacationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

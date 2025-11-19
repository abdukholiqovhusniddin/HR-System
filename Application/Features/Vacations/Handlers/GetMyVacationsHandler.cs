using Application.Commons;
using Application.DTOs.Vacations.Responses;
using Application.Exceptions;
using Application.Features.Vacations.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Vacations.Handlers;
public class GetMyVacationsHandler(IVacationRepository vacationRepository) : IRequestHandler<GetMyVacationsQuery, ApiResponse<List<VacationDtoResponse>>>
{
    private readonly IVacationRepository _vacationRepository = vacationRepository;
    public async Task<ApiResponse<List<VacationDtoResponse>>> Handle(GetMyVacationsQuery request, CancellationToken cancellationToken)
    {
        Guid userId = request.UserId;
        var vacations = await _vacationRepository.GetVacationsByUserId(userId, cancellationToken)
            ?? throw new ApiException("Vacations not found");

        var response = vacations.Adapt<List<VacationDtoResponse>>();

        return new ApiResponse<List<VacationDtoResponse>>(response);
    }
}
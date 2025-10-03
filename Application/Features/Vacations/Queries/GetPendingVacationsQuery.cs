using Application.Commons;
using Application.DTOs.Vacations.Responses;
using MediatR;

namespace Application.Features.Vacations.Queries;
public record GetPendingVacationsQuery() : IRequest<ApiResponse<List<VacationDtoResponse>>>;

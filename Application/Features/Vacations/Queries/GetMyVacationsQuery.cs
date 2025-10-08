using Application.Commons;
using Application.DTOs.Vacations.Responses;
using MediatR;

namespace Application.Features.Vacations.Queries;
public record GetMyVacationsQuery(Guid UserId) : IRequest<ApiResponse<List<VacationDtoResponse>>>;
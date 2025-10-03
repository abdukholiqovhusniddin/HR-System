using Application.Commons;
using Application.DTOs.Vacations.Responses;
using MediatR;

namespace Application.Features.Vacations.Commands;
public record RejectVacationCommand(Guid VacationId) 
    : IRequest<ApiResponse<VacationDtoResponse>>;

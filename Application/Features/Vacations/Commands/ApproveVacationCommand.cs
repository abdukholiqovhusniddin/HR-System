using Application.Commons;
using Application.DTOs.Vacations.Responses;
using MediatR;

namespace Application.Features.Vacations.Commands;
public record ApproveVacationCommand(Guid VacationId) 
    : IRequest<ApiResponse<VacationDtoResponse>>;

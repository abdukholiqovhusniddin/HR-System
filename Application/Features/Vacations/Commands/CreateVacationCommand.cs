using Application.Commons;
using Application.DTOs.Vacations.Requests;
using MediatR;

namespace Application.Features.Vacations.Commands;
public record CreateVacationCommand(Guid UserId, CreateVacationDtoRequest CreateVacationDtoRequest) 
    : IRequest<ApiResponse<Vacation>>;

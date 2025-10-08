using Application.Commons;
using Application.DTOs.Equipments.Responses;
using MediatR;

namespace Application.Features.Equipment.Queries;
public record GetEquipmentByEmployeeIdQuery(Guid EmployeeId)
    : IRequest<ApiResponse<List<EquipmentDtoResponse>>>;
using Application.Commons;
using Application.DTOs.Equipments.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Equipment.Commands;
public record UpdateEquipmentStatusCommand(Guid EquipmentId, EquipmentStatus NewStatus) 
    : IRequest<ApiResponse<EquipmentDtoResponse>>;

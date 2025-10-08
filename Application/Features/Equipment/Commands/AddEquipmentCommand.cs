using Application.Commons;
using Application.DTOs.Equipments.Requests;
using Application.DTOs.Equipments.Responses;
using MediatR;

namespace Application.Features.Equipment.Commands;
public record AddEquipmentCommand(AddEquipmentDtoRequest AddEquipmentDtoRequest)
    : IRequest<ApiResponse<EquipmentDtoResponse>>;

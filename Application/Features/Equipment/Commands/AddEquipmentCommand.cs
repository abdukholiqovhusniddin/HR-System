using Application.Commons;
using Application.DTOs.Equipments.Requests;
using Application.DTOs.Equipments.Responses;
using Application.DTOs.Vacations.Requests;
using Application.DTOs.Vacations.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Equipment.Commands;
public record AddEquipmentCommand(AddEquipmentDtoRequest AddEquipmentDtoRequest)
    : IRequest<ApiResponse<EquipmentDtoResponse>>;

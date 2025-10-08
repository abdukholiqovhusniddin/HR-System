using Application.Commons;
using Application.DTOs.Equipments.Responses;
using Application.Features.Equipment.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Equipment.Handlers;
public class AddEquipmentHandler(IEquipmentRepository equipmentRepository)
    : IRequestHandler<AddEquipmentCommand, ApiResponse<EquipmentDtoResponse>>
{
    private readonly IEquipmentRepository _equipmentRepository = equipmentRepository;
    public async Task<ApiResponse<EquipmentDtoResponse>> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = request.AddEquipmentDtoRequest;
        var newEquipment = equipment.Adapt<Equipments>();

        await _equipmentRepository.AddEquipmentAsync(newEquipment);

        var response = newEquipment.Adapt<EquipmentDtoResponse>();

        return new ApiResponse<EquipmentDtoResponse>(response);
    }
}
using Application.Commons;
using Application.DTOs.Equipments.Responses;
using Application.Features.Equipment.Commands;
using Domain.Enums;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Equipment.Handlers;
public class UpdateEquipmentStatusHandler(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEquipmentStatusCommand, ApiResponse<EquipmentDtoResponse>>
{
    private readonly IEquipmentRepository _equipmentRepository = equipmentRepository;
    public async Task<ApiResponse<EquipmentDtoResponse>> Handle(UpdateEquipmentStatusCommand request, CancellationToken cancellationToken)
    {
        Guid equipmentId = request.EquipmentId;
        EquipmentStatus newStatus = request.NewStatus;

        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId)
            ?? throw new KeyNotFoundException("Equipment not found");
        equipment.Status = newStatus;

        //await _equipmentRepository.UpdateStatusAsync(equipment);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        var response = equipment.Adapt<EquipmentDtoResponse>();
        return new ApiResponse<EquipmentDtoResponse>(response);

    }
}

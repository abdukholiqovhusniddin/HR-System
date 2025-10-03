using Domain.Enums;

namespace Application.DTOs.Equipments.Responses;
public class EquipmentDtoResponse
{
    public required string FullName { get; set; }
    public required string Position { get; set; }
    public required string Department { get; set; }

    public required string EquipmentType { get; set; }
    public required string Model { get; set; }
    public required string InventoryNumber { get; set; }
    public required EquipmentStatus Status { get; set; } = EquipmentStatus.OnHand;
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
}
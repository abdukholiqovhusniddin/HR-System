using Domain.Enums;

namespace Application.DTOs.Equipments.Responses;
public class EquipmentDtoResponse
{
    public required string Type { get; set; }
    public required string Model { get; set; }
    public required string InventoryNumber { get; set; }
    public required EquipmentStatus Status { get; set; }
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
}
using Domain.Enums;

namespace Domain.Entities;
public class Equipments: EmployeeBaseEntity
{
    public required string Type { get; set; }

    public required string Model { get; set; }

    public required string InventoryNumber { get; set; }

    public DateTime AssignmentDate { get; set; } = DateTime.Now;

    public required EquipmentStatus Status { get; set; } = EquipmentStatus.OnHand;
}
namespace HR_System.Entities;
public class EquipmentAssignment: EmployeeBaseEntity
{
    public string? Type { get; set; }

    public string? Model { get; set; }

    public string? InventoryNumber { get; set; }

    public DateTime AssignmentDate { get; set; }

    public string? Status { get; set; }
}
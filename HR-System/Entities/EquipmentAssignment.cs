namespace HR_System.Entities;
public class EquipmentAssignment: BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public string? Type { get; set; }

    public string? Model { get; set; }

    public string? InventoryNumber { get; set; }

    public DateTime AssignmentDate { get; set; }

    public string? Status { get; set; }
}
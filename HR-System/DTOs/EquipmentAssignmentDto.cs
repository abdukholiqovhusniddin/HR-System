namespace HR_System.DTOs;
public class EquipmentAssignmentDto
{
    public Guid EmployeeId { get; set; }
    public string? Type { get; set; }
    public string? Model { get; set; }
    public string? InventoryNumber { get; set; }
    public DateTime AssignmentDate { get; set; }
    public string? Status { get; set; }
}
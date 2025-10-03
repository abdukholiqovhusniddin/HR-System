namespace Application.DTOs.Equipments.Requests;
public class AddEquipmentDtoRequest
{
    public required Guid EmployeeId { get; set; }
    public required string Type { get; set; }
    public required string Model { get; set; }
    public required string InventoryNumber { get; set; }
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
}

namespace HR_System.DTOs;
public class VacationRequestDto
{
    public Guid EmployeeId { get; set; }
    public string? VacationType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Status { get; set; }
}
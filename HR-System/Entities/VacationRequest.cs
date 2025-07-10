namespace HR_System.Entities;
public class VacationRequest: BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public string? VacationType { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? Status { get; set; }
}

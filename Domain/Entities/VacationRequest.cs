namespace HR_System.Entities;
public class VacationRequest: EmployeeBaseEntity
{
    public string? VacationType { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? Status { get; set; }
}
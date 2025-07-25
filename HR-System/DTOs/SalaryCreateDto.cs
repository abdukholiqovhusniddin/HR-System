namespace HR_System.DTOs;
public class SalaryCreateDto
{
    public Guid EmployeeId { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal? Bonus { get; set; }
    public decimal? Deduction { get; set; }
    public DateTime StartPeriod { get; set; }
    public DateTime EndPeriod { get; set; }
}

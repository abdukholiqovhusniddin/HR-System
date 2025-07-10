namespace HR_System.Entities;
public class Salary: BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? Deduction { get; set; }

    public DateTime StartPeriod { get; set; }
    public DateTime EndPeriod { get; set; }
}

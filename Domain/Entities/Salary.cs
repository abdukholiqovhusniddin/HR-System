namespace HR_System.Entities;
public class Salary: EmployeeBaseEntity
{
    public decimal BaseSalary { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? Deduction { get; set; }

    public DateTime StartPeriod { get; set; }
    public DateTime EndPeriod { get; set; }
}
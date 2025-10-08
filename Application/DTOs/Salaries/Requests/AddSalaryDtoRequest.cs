namespace Application.DTOs.Salaries.Requests;
public class AddSalaryDtoRequest
{
    public required Guid EmployeeId { get; set; }
    public required decimal BaseSalary { get; set; }
    public decimal? Bonus { get; set; }
    public decimal? Deduction { get; set; }
    public required DateTime StartPeriod { get; set; }
    public required DateTime EndPeriod { get; set; }
}
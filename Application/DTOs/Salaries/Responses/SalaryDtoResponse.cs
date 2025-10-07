namespace Application.DTOs.Salaries.Responses;
public class SalaryDtoResponse
{
    public required decimal BaseSalary { get; set; }
    public decimal? Bonus { get; set; }
    public decimal? Deduction { get; set; }
    public required DateTime StartPeriod { get; set; }
    public required DateTime EndPeriod { get; set; }
    public decimal? FinalAmount  => BaseSalary + (Bonus ?? 0) - (Deduction ?? 0);
}

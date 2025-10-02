namespace Application.DTOs.Salaries.Responses;
public class SalaryEmployeeAndHistoryDtoResponse
{
    public required string FullName { get; set; }
    public required string Position { get; set; }
    public required string Department { get; set; }
    public required decimal BaseSalary { get; set; }
    public decimal? Bonus { get; set; }
    public decimal? Deduction { get; set; }
    public required DateTime StartPeriod { get; set; }
    public required DateTime EndPeriod { get; set; }
}

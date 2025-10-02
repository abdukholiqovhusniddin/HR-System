namespace Application.DTOs.Vacations.Responses;

public class VacationDtoResponse
{
    public required string FullName { get; set; }
    public required string Position { get; set; }
    public required string Department { get; set; }
    public string? VacationType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Status { get; set; }
}
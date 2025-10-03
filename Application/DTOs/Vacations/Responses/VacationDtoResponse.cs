using Domain.Enums;

namespace Application.DTOs.Vacations.Responses;

public class VacationDtoResponse
{
    public required string FullName { get; set; }
    public required string Position { get; set; }
    public required string Department { get; set; }
    public required VacationType VacationType { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required VacationStatus Status { get; set; }
}
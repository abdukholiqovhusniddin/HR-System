using Domain.Enums;

namespace Application.DTOs.Vacations.Requests;
public class CreateVacationDtoRequest
{
    public required VacationType VacationType { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

}
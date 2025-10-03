using Domain.Enums;

namespace Domain.Entities;
public class Vacations: EmployeeBaseEntity
{
    public required VacationType VacationType { get; set; }

    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public required VacationStatus Status { get; set; }
}
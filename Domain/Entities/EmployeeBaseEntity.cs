namespace Domain.Entities;
public abstract class EmployeeBaseEntity: BaseEntity
{
    public required Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
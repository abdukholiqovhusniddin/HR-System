namespace HR_System.Entities;
public abstract class EmployeeBaseEntity: BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
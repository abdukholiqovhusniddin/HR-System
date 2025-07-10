namespace HR_System.Entities;

public enum UserRole
{
    Employee,
    HR,
    Accountant,
    Manager,
    Admin
}
public class User: BaseEntity
{
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public UserRole Role { get; set; }
}
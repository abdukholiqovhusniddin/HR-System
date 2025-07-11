namespace HR_System.Entities;

public enum UserRole
{
    Employee,
    HR,
    Accountant,
    Manager,
    Admin
}
public class User: EmployeeBaseEntity
{
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }

    public UserRole Role { get; set; }
}
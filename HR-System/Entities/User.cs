namespace HR_System.Entities;

public enum UserRole
{
    Employee, // 0
    HR,       // 1
    Accountant,// 2
    Manager,  // 3
    Admin     // 4
}
public class User: BaseEntity
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public Employee? EmployeeProfile { get; set; }
}
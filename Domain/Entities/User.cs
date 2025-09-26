using Domain.Enums;

namespace Domain.Entities;

public class User: BaseEntity
{
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public UserRole Role { get; set; }

    public Employee? EmployeeProfile { get; set; }
}
using Domain.Commons;

namespace Domain.Entities;
public class Employee : BaseEntity
{
    public required string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhotoUrl { get; set; }
    public EmployeeFile Image { get; set; } = default!;

    public required string Email { get; set; }
    public bool IsEmailPublic { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Telegram { get; set; }
    public bool IsTelegramPublic { get; set; }

    public required string Position { get; set; }
    public  required string Department { get; set; }
    public DateTime HireDate { get; set; }

    public required string PassportInfo { get; set; }

    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public ICollection<Employee>? Subordinates { get; set; }
    public ICollection<Contract>? Contracts { get; set; }
    public ICollection<Salary>? Salaries { get; set; }
    public ICollection<Vacation>? Vacations { get; set; }
    public ICollection<EquipmentAssignment>? Equipments { get; set; }
}
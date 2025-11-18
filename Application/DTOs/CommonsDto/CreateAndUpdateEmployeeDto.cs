using Domain.Enums;

namespace Application.DTOs.CommonsDto;

public abstract class CreateAndUpdateEmployeeDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public UserRole Role { get; set; } = UserRole.Employee;

    public required string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public required string PhoneNumber { get; set; }
    public required string Telegram { get; set; }

    public bool IsEmailPublic { get; set; } = false;
    public bool IsTelegramPublic { get; set; } = false;

    public required string Position { get; set; }
    public required string Department { get; set; }

    public required DateTime HireDate { get; set; } = DateTime.UtcNow;

    public required string PassportInfo { get; set; }

    public Guid? ManagerId { get; set; } = null;
}
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.CommonsDto;
public abstract class CreateAndUpdateEmployeeDto
{
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public required string Username { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public required string Email { get; set; }

    [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role.")]
    public UserRole Role { get; set; } = UserRole.Employee;

    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime DateOfBirth { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Telegram { get; set; }
    public bool IsEmailPublic { get; set; } = false;
    public bool IsTelegramPublic { get; set; } = false;

    public required string Position { get; set; }
    public required string Department { get; set; }

    public required DateTime HireDate { get; set; } = DateTime.UtcNow;

    public required string PassportInfo { get; set; }

    public Guid? ManagerId { get; set; }
}
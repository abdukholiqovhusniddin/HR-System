using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Users.Responses;
public class UserResponseDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserRole Role { get; set; }
    public string? FullName { get; set; }
    public required string Photo { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Telegram { get; set; }
    public bool IsTelegramPublic { get; set; }
    public bool IsEmailPublic { get; set; }
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime HireDate { get; set; }
    public string? PassportInfo { get; set; }
    public Guid? ManagerId { get; set; }
}
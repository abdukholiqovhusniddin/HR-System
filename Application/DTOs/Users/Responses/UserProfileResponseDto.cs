using Domain.Enums;

namespace Application.DTOs.Users.Responses;
public class UserProfileResponseDto
{
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public UserRole Role { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Email { get; set; }
    public bool IsEmailPublic { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Telegram { get; set; }
    public bool IsTelegramPublic { get; set; }
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime HireDate { get; set; }
    public string? PassportInfo { get; set; }
    public int? Age { get; internal set; }
    
    public static UserProfileResponseDto FromEmployee(Employee e) => new()
    {
        FullName = e.FullName,
        DateOfBirth = e.DateOfBirth,
        IsEmailPublic = e.IsEmailPublic,
        PhoneNumber = e.PhoneNumber,
        Telegram = e.Telegram,
        IsTelegramPublic = e.IsTelegramPublic,
        Position = e.Position,
        Department = e.Department,
        HireDate = e.HireDate,
        PassportInfo = e.PassportInfo,
        PhotoUrl = e.PhotoUrl,
        Role = e.User.Role,
        Age = DateTime.Today.Year - e.DateOfBirth.Year -
              (e.DateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - e.DateOfBirth.Year)) ? 1 : 0)
    };
}

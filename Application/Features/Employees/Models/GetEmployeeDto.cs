using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Models;
public class GetEmployeeDto
{
    public string? FullName { get; set; }
    public IFormFile? Photo { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Telegram { get; set; }
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime HireDate { get; set; }
    public string? PassportInfo { get; set; }
    public Guid ManagerId { get; set; }
    public bool IsEmailPublic { get; set; }
    public bool IsTelegramPublic { get; set; }
    public ICollection<int>? SubordinateIds { get; set; } = [];
}

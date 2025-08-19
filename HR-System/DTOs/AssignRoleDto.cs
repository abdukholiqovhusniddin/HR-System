using HR_System.Entities;

namespace HR_System.DTOs;
public class AssignRoleDto
{
    public string? Username { get; set; }
    public UserRole Role { get; set; }
}

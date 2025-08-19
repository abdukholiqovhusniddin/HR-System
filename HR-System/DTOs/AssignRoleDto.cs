using System.ComponentModel.DataAnnotations;
using HR_System.Entities;

namespace HR_System.DTOs;
public class AssignRoleDto
{
    [Required]
    public string? Username { get; set; }

    [Required]
    [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role.")]
    public UserRole Role { get; set; }
}

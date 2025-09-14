using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Requests;
public class AssignRoleRequestsDto
{
    public required string Username { get; set; }

    [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role.")]
    public required UserRole Role { get; set; }
}

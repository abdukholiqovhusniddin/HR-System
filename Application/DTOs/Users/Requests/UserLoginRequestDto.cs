using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.Requests;
public class UserLoginRequestDto
{
    public required string Username { get; set; }

    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public required string Password { get; set; }
}

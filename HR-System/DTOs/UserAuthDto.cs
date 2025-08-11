using System.ComponentModel.DataAnnotations;
using HR_System.Entities;

namespace HR_System.DTOs;
public class UserAuthDto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; } = UserRole.Employee  ;
    }
    public class UserDto(string? username, string? email, UserRole role)
    {
        public string? Username { get; set; } = username;
        public string? Email { get; set; } = email;
        public UserRole Role { get; set; } = role;
    }

    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }
    }

    public class UserProfileDto
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public bool IsEmailPublic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Telegram { get; set; }
        public bool IsTelegramPublic { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }
        public string? PassportInfo { get; set; }
        public Guid ManagerId { get; set; }
        public int? Age { get; internal set; }
    }
}

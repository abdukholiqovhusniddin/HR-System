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

        //[MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        //[Required(ErrorMessage = "Password is required")]
        //public string? Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; } = UserRole.Employee;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string? FullName { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [StringLength(int.MaxValue, ErrorMessage = "Photo URL cannot exceed maximum length")]
        public string? PhotoUrl { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Telegram is required")]
        public string? Telegram { get; set; }
        public bool IsEmailPublic { get; set; } = false;
        public bool IsTelegramPublic { get; set; } = false;

        [Required(ErrorMessage = "Position is required")]
        public string? Position { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "Hire date is required")]
        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Passport info is required")]
        public string? PassportInfo { get; set; }

        [Required(ErrorMessage = "ManagerUsername is required")]
        public string? ManagerUsername { get; set; }
    }
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
        public string? FullName { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Telegram { get; set; }
        public bool IsTelegramPublic { get; set; }
        public bool IsEmailPublic { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }
        public string? PassportInfo { get; set; }
        public string? ManagerName { get; set; }
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
    }
}

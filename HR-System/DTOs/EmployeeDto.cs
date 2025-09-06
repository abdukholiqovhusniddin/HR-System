namespace HR_System.DTOs;
public class EmployeeDto
{
    public class EmployeeCreateDto
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

    public class EmployeeUpdateDto : EmployeeCreateDto
    {
        public Guid Id { get; set; }
    }

    public class EmployeeDirectoryDto
    {
        public string? FullName { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Today.Year - DateOfBirth.Year -
        (DateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - DateOfBirth.Year)) ? 1 : 0);
    }
}

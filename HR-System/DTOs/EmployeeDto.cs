namespace HR_System.DTOs;
public class EmployeeDto
{
    public class EmployeeCreateDto
    {
        public string? FullName { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Telegram { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }
        public string? PassportInfo { get; set; }
        public int? ManagerId { get; set; }
        public bool IsEmailPublic { get; set; }
        public bool IsTelegramPublic { get; set; }
    }

    public class EmployeeUpdateDto : EmployeeCreateDto
    {
        public int Id { get; set; }
    }

    public class EmployeeDirectoryDto
    {
        public string? FullName { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
    }
}

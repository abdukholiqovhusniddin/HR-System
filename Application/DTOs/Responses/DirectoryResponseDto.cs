namespace Application.DTOs.Responses;
public class DirectoryResponseDto(string employeeName, string? position, string? department)
{
    public string EmployeeName { get; set; } = employeeName;
    public string? Position { get; set; } = position;
    public string? Department { get; set; } = department;
}

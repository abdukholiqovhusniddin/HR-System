namespace HR_System.Entities;
public class DataFile(Guid EmployeeId, string Name,
    string Url, long Size, string Extension)
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; } = EmployeeId;
    public string Name { get; set; } = Name;
    public string Url { get; set; } = Url;
    public long Size { get; set; } = Size;
    public string Extension { get; set; } = Extension;
}

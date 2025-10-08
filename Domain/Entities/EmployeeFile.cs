using Domain.Commons;

namespace Domain.Entities;
public class EmployeeFile: DataFile
{
    public required Guid EmployeeId { get; set; }
}
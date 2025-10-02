
namespace Domain.Interfaces;
public interface ISalariesRepository
{
    Task CreateAsync(Salary newSalary);
    Task<Salary?> GetByEmployeeId(Guid employeeId);
    Task<List<Salary>> GetHistoryByEmployeeId(Guid salaryEmployee);
}

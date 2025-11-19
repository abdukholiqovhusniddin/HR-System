
namespace Domain.Interfaces;
public interface ISalariesRepository
{
    Task CreateAsync(Salary newSalary);
    Task<Salary?> GetByEmployeeId(Guid employeeId, CancellationToken cancellationToken);
    Task<List<Salary>> GetHistoryByEmployeeId(Guid salaryEmployee);
    Task<List<Salary>> GetSalaryReport();
    Task<bool> IsEmployeeAktive(Guid employeeId, CancellationToken cancellationToken);
}
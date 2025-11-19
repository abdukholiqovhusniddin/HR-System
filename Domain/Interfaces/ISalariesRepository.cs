
namespace Domain.Interfaces;
public interface ISalariesRepository
{
    Task CreateAsync(Salary newSalary);
    Task<Salary?> GetByEmployeeId(Guid employeeId, CancellationToken cancellationToken);
    Task<List<Salary>> GetHistoryByEmployeeId(Guid salaryEmployee, CancellationToken cancellationToken);
    Task<List<Salary>> GetSalaryReport(CancellationToken cancellationToken);
    Task<bool> IsEmployeeAktive(Guid employeeId, CancellationToken cancellationToken);
}
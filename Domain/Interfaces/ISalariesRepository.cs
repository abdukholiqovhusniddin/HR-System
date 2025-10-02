
namespace Domain.Interfaces;
public interface ISalariesRepository
{
    Task CreateAsync(Salary newSalary);
    Task<List<Salary>> GetByEmployeeId(Guid employeeId);
}

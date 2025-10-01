
namespace Domain.Interfaces;
public interface ISalariesRepository
{
    Task<Salary?> GetByEmployeeId(Guid employeeId);
}

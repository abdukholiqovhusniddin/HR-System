
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task<bool> ExistsAsync(Guid employeeId);
    Task<Contract> GetByEmployeeId(Guid employeeId);
}

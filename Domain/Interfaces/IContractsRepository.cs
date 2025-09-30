
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task<Contract> CreateAsync(Contract newContract);
    Task<Contract> GetByEmployeeId(Guid employeeId);
}

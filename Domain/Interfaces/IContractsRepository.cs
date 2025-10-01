
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task CreateAsync(Contract newContract);
    Task<Contract> GetByEmployeeId(Guid employeeId);
    Task<Contract> GetContractById(Guid contractId);
    Task UpdateAsync(Contract updateContract);
}

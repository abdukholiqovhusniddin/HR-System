
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task CreateAsync(Contract newContract);
    Task<List<Contract>> GetByEmployeeId(Guid employeeId);
    Task<Contract?> GetContractById(Guid contractId);
    Task<bool> IsEmployeeAktive(Guid employeeId);
}
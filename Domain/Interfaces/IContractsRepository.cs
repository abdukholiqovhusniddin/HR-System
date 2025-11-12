
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task CreateAsync(Contract newContract);
    Task<List<Contract>> GetAllByEmployeeIdAsync(Guid employeeId);
    Task<Contract?> GetContractById(Guid contractId);
    Task<bool> IsEmployeeAktive(Guid employeeId);
}
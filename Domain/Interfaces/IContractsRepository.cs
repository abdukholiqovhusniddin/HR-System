
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task CreateAsync(Contract newContract);
    Task<List<Contract>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<Contract?> GetContractById(Guid contractId, CancellationToken cancellationToken = default);
    Task<bool> IsEmployeeActive(Guid employeeId, CancellationToken cancellationToken = default);
}
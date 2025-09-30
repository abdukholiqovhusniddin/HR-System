
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task CreateAsync(Contract newContract);
    Task<Contract?> ExistAsync(global::Application.DTOs.Contract.Requests.UpdateContractDtoRequest update);
    Task<Contract> GetByEmployeeId(Guid employeeId);
    Task UpdateAsync(Contract updateContract);
}

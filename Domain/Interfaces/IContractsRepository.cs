
namespace Domain.Interfaces;
public interface IContractsRepository
{
    Task<Contract> GetByEmployeeId(Guid employeeId);
}

using System.Threading;

namespace Domain.Interfaces;
public interface IEmployeesRepository
{
    Task<IEnumerable<Employee>> GetAllDirectory(CancellationToken cancellationToken);
    Task<Employee?> GetById(Guid id, CancellationToken cancellation, bool t);
    Task<EmployeeFile> GetImgByEmployeeId(Guid id);
}
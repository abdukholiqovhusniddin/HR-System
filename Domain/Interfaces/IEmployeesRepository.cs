namespace Domain.Interfaces;
public interface IEmployeesRepository
{
    Task Delete(Employee employee);
    Task<IEnumerable<Employee>> GetAllDirectory();
    Task<Employee?> GetById(Guid id);
}

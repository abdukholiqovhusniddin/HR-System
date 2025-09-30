namespace Domain.Interfaces;
public interface IEmployeesRepository
{
    Task<IEnumerable<Employee>> GetAllDirectory();
    Task<Employee?> GetById(Guid id);
    Task Update(Employee employee);
}

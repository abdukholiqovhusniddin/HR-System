using Domain.Entities;

namespace Domain.Interfaces;
public interface IEmployerRepository
{
    Task<Employee> CreateAsync(Employee newEmployer);
}

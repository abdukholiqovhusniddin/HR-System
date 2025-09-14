using HR_System.Entities;

namespace Domain.Interfaces;
public interface IEmployerRepository
{
    Task<UserDto?> CreateAsync(Employee newEmployer);
}

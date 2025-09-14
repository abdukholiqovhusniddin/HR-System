using Domain.Entities;

namespace Domain.Interfaces;
public interface IEmployerRepository
{
    Task<UserDto?> CreateAsync(Employee newEmployer);
}

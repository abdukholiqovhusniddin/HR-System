using HR_System.Entities;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces.Repository;
public interface IEmployerRepository
{
    Task<UserDto?> CreateAsync(Employee newEmployer);
}

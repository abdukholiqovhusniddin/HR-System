using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces.Repository;
public interface IEmployerRepository
{
    Task<UserDto?> CreateAsync(Guid userId, UserRegisterDto userRegisterDto);
}

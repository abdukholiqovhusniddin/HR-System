using HR_System.DTOs;
using HR_System.Entities;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces;
public interface IUserRepository
{
    Task<Guid> CreateAsync(User user);
    Task<bool> ExistsAsync(string? username);
    Task<User?> GetByUsernameAsync(string? username, bool includeEmployeeProfile = false    );
    Task UpdateAsync(User user);
}

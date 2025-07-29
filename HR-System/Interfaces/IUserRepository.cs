using HR_System.Entities;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Interfaces;
public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<bool> ExistsAsync(string? username);
    Task<User?> GetByUsernameAsync(string? username);
    Task<User?> GetUserInfoByRoleAsync(Guid userid, string role);
}

using HR_System.DTOs;
using HR_System.Entities;

namespace HR_System.Interfaces;
public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<bool> ExistsAsync(string? username);
    Task<User?> GetByUsernameAsync(string? username);
    //Task<User?> GetUserInfoByRoleAsync(Guid userid, string role);
}

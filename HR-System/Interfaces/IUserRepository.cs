using HR_System.Entities;

namespace HR_System.Interfaces;
public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<bool> ExistsAsync(string? username);
    Task<User?> GetByUsernameAsync(string? username);
    Task<object?> GetUserInfoByRoleAsync(User user, string role);
}

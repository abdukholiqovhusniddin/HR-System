using Domain.Entities;

namespace Domain.Interfaces;
public interface IUserRepository
{
    Task<Guid> CreateAsync(User user);
    Task<bool> ExistsAsync(string? username);
    Task<User?> GetByUsernameAsync(string? username, bool includeEmployeeProfile = false);
    Task UpdateAsync(User user);
}

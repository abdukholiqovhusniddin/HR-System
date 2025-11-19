using Domain.Entities;

namespace Domain.Interfaces;
public interface IUserRepository
{
    Task<Guid> CreateAsync(User user);
    Task<bool> ExistsAsync(string? username);
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByUsernameAsync(string? username, CancellationToken cancellationToken, bool includeEmployeeProfile = false);
    Task UpdateAsync(User user);
}
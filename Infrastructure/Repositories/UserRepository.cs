using Application.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Guid> CreateAsync(User user)
    {
        if (user is null)
            throw new ApiException("User cannot be null.");
        await _context.Users.AddAsync(user);
        return user.Id;
    }

    public async Task<bool> ExistsAsync(string? usernameOrEmail)
    {
        if (string.IsNullOrWhiteSpace(usernameOrEmail))
            throw new ApiException("Username or email cannot be null or empty.");
        return await _context.Users.Include(u => u.EmployeeProfile)
            .AnyAsync(n => n.Username == usernameOrEmail
            && n.EmployeeProfile.Email == usernameOrEmail && n.EmployeeProfile.IsActive);
    }

    public async Task<User?> GetByUsernameAsync(string? username, bool includeEmployeeProfile = false)
    {
        IQueryable<User> query = _context.Users;

        if (includeEmployeeProfile)
            query = query.Include(u => u.EmployeeProfile);

        return await query.FirstOrDefaultAsync(u => u.Username == username && u.EmployeeProfile.IsActive);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
using HR_System.Data;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repository;
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string? usernameOrEmail)
    {
        if (string.IsNullOrWhiteSpace(usernameOrEmail))
            throw new ApiException("Username or email cannot be null or empty.");
        return await _context.Users.AnyAsync(n => n.Username == usernameOrEmail || n.Email == usernameOrEmail);
    }

    public async Task<User?> GetByUsernameAsync(string? username) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(n => n.Username == username);

    public async Task<object?> GetUserInfoByRoleAsync(User user, string role)
    {
        switch (role)
        {
            case "Employee":
                return new
                {
                    user.Username,
                    user.Email,
                    user.Role,
                    user.Id
                };
            case "HR":
                return new
                {
                    user.Username,
                    user.Email,
                    user.Role,
                    user.Id,
                    user.EmployeeProfile
                };
            case "Accountant":
                return new
                {
                    user.Username,
                    user.Email,
                    user.Role,
                    user.Id,
                    user.EmployeeProfile
                };
            case "Manager":
                return new
                {
                    user.Username,
                    user.Email,
                    user.Role,
                    user.Id,
                    user.EmployeeProfile
                };
            case "Admin":
                return new
                {
                    user.Username,
                    user.Email,
                    user.Role,
                    user.Id
                };
            default:
                throw new ApiException("Invalid role specified.");
        }
    }
}

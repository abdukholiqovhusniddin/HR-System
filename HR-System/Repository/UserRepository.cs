using System.Linq;
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
        if (user == null)
            throw new ApiException("User cannot be null.");
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

    public IQueryable<User> QueryUserById(Guid id, bool track)
    {
        var query = _context.Users.Where(x => x.Id == id);
        return track ? query.AsTracking() : query.AsNoTracking();
    }
    public async Task<User?> GetUserInfoByRoleAsync(Guid id, string role)
    {
        var user = QueryUserById(id, true);

        return role switch
        {
            "Employee" => await user
                                .Include(u => u.EmployeeProfile!)
                                    .ThenInclude(e => e.FullName)
                                .Include(u => u.EmployeeProfile!.Subordinates!)
                                .FirstOrDefaultAsync(),
            "HR" => await user
                                .Include(u => u.EmployeeProfile!)
                                .ThenInclude(e => e.Manager)
                                .Include(u => u.EmployeeProfile!.Subordinates!)
                                .FirstOrDefaultAsync(),
            "Accountant" => await user
                                .Include(u => u.EmployeeProfile!)
                                    .ThenInclude(e => e.Contracts!)
                                .Include(u => u.EmployeeProfile!.Department)
                                .FirstOrDefaultAsync(),
            "Manager" => await user
                                .Include(u => u.EmployeeProfile!)
                                    .ThenInclude(e => e.Manager)
                                .FirstOrDefaultAsync(),
            "Admin" => await user
                                .Include(u => u.EmployeeProfile!)
                                    .ThenInclude(e => e.Manager)
                                .Include(u => u.EmployeeProfile!.Subordinates!)
                                .FirstOrDefaultAsync(),
            _ => throw new ApiException("Invalid role specified."),
        };
    }
}

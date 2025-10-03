using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class VacationRepository(AppDbContext context) : IVacationRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateVacationAsync(Guid userId, Vacation vacation) =>
        await _context.Vacations.AddAsync(vacation);

    public async Task<Guid> GetEmployeeIdByUserIdAsync(Guid userId) =>
        await _context.Employees
            .Where(e => e.UserId == userId && e.IsActive)
            .Select(e => e.Id)
            .FirstOrDefaultAsync();

    public async Task<List<Vacation>> GetPendingVacationsAsync() =>
        await _context.Vacations.Where(s => s.Status == Domain.Enums.VacationStatus.Pending
            && s.Employee.IsActive).ToListAsync();

    public async Task<Vacation?> GetVacationsByUserId(Guid userId) =>
        await _context.Vacations.Include(v => v.Employee)
            .FirstOrDefaultAsync(v => v.Employee.UserId == userId && v.Employee.IsActive);
}

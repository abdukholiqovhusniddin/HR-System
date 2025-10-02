using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class SalariesRepository(AppDbContext context) : ISalariesRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(Salary newSalary) =>
        await _context.Salaries.AddAsync(newSalary);

    public async Task<Salary?> GetByEmployeeId(Guid employeeId) =>
        await _context.Salaries.Include(s => s.Employee)
            .Where(s => s.EmployeeId == employeeId && s.Employee.IsActive)
            .OrderByDescending(s => s.StartPeriod).FirstOrDefaultAsync();

    public async Task<List<Salary>> GetHistoryByEmployeeId(Guid salaryEmployee) =>
        await _context.Salaries.Include(s => s.Employee)
            .Where(s => s.EmployeeId == salaryEmployee && s.Employee.IsActive)
            .OrderBy(s => s.StartPeriod).ToListAsync();

    public async Task<List<Salary?>> GetSalaryReport() =>
        await _context.Salaries.Include(s => s.Employee)
            .Where(s => s.Employee.IsActive)
            .GroupBy(s => s.EmployeeId)
            .Select(g => g.OrderByDescending(s => s.StartPeriod)
                      .FirstOrDefault())                
            .Where(s => s != null)         
            .ToListAsync();

}

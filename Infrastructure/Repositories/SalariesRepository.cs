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

    public async Task<List<Salary>> GetByEmployeeId(Guid employeeId) =>
        await _context.Salaries.Where(s => s.EmployeeId == employeeId
        && s.Employee.IsActive).ToListAsync();
}

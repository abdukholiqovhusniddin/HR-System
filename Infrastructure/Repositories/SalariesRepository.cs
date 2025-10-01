using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class SalariesRepository(AppDbContext context) : ISalariesRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Salary?> GetByEmployeeId(Guid employeeId) =>
        await _context.Salaries.FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.Employee.IsActive);
}

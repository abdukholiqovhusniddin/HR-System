using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class EmployeesRepository(AppDbContext context) : IEmployeesRepository
{
    private readonly AppDbContext _context = context;

    public async Task Delete(Employee employee)
    {
        await _context.Employees.Where(e => e.Id == employee.Id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllDirectory() =>
        await _context.Employees.Where(a => a.User.Role != UserRole.Admin && a.IsActive).ToListAsync();

    public async Task<Employee?> GetById(Guid id) =>
        await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }
}

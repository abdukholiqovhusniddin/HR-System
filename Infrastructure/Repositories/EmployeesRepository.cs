using HR_System.Data;
using HR_System.DTOs;
using HR_System.Entities;
using HR_System.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repository;
public class EmployeesRepository(AppDbContext context) : IEmployeesRepository
{
    private readonly AppDbContext _context = context;
    public async Task<IEnumerable<Employee>> GetAllDirectory() =>
        await _context.Employees.Where(a => a.User.Role != UserRole.Admin).ToListAsync();

    public async Task<Employee?> GetById(Guid id) =>
        await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
}

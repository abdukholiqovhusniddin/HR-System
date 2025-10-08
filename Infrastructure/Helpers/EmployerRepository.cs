using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;

namespace Infrastructure.Helpers;
public class EmployerRepository(AppDbContext context, IFileService fileService) : IEmployerRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Employee> CreateAsync(Employee newEmployer)
    {
        await _context.Employees.AddAsync(newEmployer);
        return newEmployer;
    }
}
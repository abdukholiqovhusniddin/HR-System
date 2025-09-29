using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ContractsRepository(AppDbContext context): IContractsRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsAsync(Guid employeeId)
    {
        return await _context.Contracts
        .AnyAsync(c => c.EmployeeId == employeeId
                       && c.IsDeleted        // contract is deleted
                       && c.Employee.IsActive // employee is active
                  );
    }

    public async Task<Contract> GetByEmployeeId(Guid employeeId) =>
        await _context.Contracts.FirstOrDefaultAsync(c => c.EmployeeId == employeeId 
            && c.Employee.IsActive);
}

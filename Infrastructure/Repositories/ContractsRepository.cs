using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ContractsRepository(AppDbContext context) : IContractsRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(Contract newContract) =>
        await _context.Contracts.AddAsync(newContract);

    public async Task<List<Contract>> GetByEmployeeId(Guid employeeId) =>
        await _context.Contracts.Include(c => c.Employee)
            .Where(c => c.EmployeeId == employeeId
            && c.Employee.IsActive && c.IsAktive).ToListAsync();

    public async Task<Contract?> GetContractById(Guid contractId) =>
        await _context.Contracts.Include(c => c.Employee)
            .FirstOrDefaultAsync(c => c.Id == contractId &&
            c.Employee.IsActive && c.IsAktive);

    public async Task<bool> IsEmployeeAktive(Guid employeeId) =>
        await _context.Employees.AnyAsync(e => e.Id == employeeId && e.IsActive);
}
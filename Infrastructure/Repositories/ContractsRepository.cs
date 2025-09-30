using Application.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ContractsRepository(AppDbContext context): IContractsRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(Contract newContract)
    {
        if (newContract is null)
            throw new ApiException("Contract is not null");
        await _context.Contracts.AddAsync(newContract);
    }

    public async Task<Contract> GetByEmployeeId(Guid employeeId) =>
        await _context.Contracts.FirstOrDefaultAsync(c => c.EmployeeId == employeeId 
            && c.Employee.IsActive);
}

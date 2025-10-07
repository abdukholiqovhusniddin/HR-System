using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class EquipmentRepository(AppDbContext appDbContext) : IEquipmentRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task AddEquipmentAsync(Equipments newEquipment)
    {
        await _context.Equipments.AddAsync(newEquipment);
        await _context.SaveChangesAsync();
    }

    public async Task<Equipments?> GetByIdAsync(Guid equipmentId) =>
        await _context.Equipments.Include(e => e.Employee)
            .FirstOrDefaultAsync(e => e.Id == equipmentId && e.Employee.IsActive);

    public async Task<List<Equipments>> GetEquipmentByEmployeeId(Guid employeeId) =>
        await _context.Equipments.Include(e => e.Employee)
        .Where(e => e.EmployeeId == employeeId && e.Employee.IsActive).ToListAsync();
}

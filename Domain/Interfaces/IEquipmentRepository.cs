
namespace Domain.Interfaces;
public interface IEquipmentRepository
{
    Task AddEquipmentAsync(Equipments newEquipment);
    Task<Equipments?> GetByIdAsync(Guid equipmentId, CancellationToken cancellationToken);
    Task<List<Equipments>> GetEquipmentByEmployeeId(Guid employeeId, CancellationToken cancellationToken);
}
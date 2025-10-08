
namespace Domain.Interfaces;
public interface IEquipmentRepository
{
    Task AddEquipmentAsync(Equipments newEquipment);
    Task<Equipments?> GetByIdAsync(Guid equipmentId);
    Task<List<Equipments>> GetEquipmentByEmployeeId(Guid employeeId);
}
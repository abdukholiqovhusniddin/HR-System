
namespace Domain.Interfaces;
public interface IEquipmentRepository
{
    Task<List<Equipments>> GetEquipmentByEmployeeId(Guid employeeId);
}

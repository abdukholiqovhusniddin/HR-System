
namespace Domain.Interfaces;
public interface IVacationRepository
{
    Task CreateVacationAsync(Guid userId, Vacation vacation);
    Task<Guid> GetEmployeeIdByUserIdAsync(Guid userId);
    Task<Vacation?> GetVacationsByUserId(Guid userId);
}

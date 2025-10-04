
namespace Domain.Interfaces;
public interface IVacationRepository
{
    Task<Vacation?> ApproveAndRejectVacation(Guid vacationId);
    Task CreateVacationAsync(Guid userId, Vacation vacation);
    Task<Guid> GetEmployeeIdByUserIdAsync(Guid userId);
    Task<List<Vacation>> GetPendingVacationsAsync();
    Task<Vacation?> GetVacationsByUserId(Guid userId);
}
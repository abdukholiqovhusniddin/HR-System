
namespace Domain.Interfaces;
public interface IVacationRepository
{
    Task<Vacation?> GetVacationsByUserId(Guid userId);
}

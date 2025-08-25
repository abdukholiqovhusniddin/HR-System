
namespace HR_System.Interfaces.Repository;
public interface IEmployerRepository
{
    Task CreateAsync(Guid userId);
}

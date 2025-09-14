using HR_System.Data;
using HR_System.Interfaces.Repository;

namespace HR_System.Repository;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
       return await context.SaveChangesAsync(cancellationToken);
    }
}

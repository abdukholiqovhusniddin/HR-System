using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;

namespace Infrastructure.Repositories;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}

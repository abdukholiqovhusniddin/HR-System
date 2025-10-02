using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;

namespace Infrastructure.Repositories;
public class VacationRepository(AppDbContext context) : IVacationRepository
{
    private readonly AppDbContext _context = context;
}

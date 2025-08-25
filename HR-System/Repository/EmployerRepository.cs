using HR_System.Data;
using HR_System.Interfaces.Repository;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Repository;
public class EmployerRepository(AppDbContext context) : IEmployerRepository
{
    private readonly AppDbContext _context = context;
    public async Task<UserDto?> CreateAsync(Guid userId, UserRegisterDto userRegisterDto)
    {
        
    }
}

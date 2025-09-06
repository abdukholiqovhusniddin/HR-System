using HR_System.Data;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Repository;
public class EmployerRepository(AppDbContext context) : IEmployerRepository
{
    private readonly AppDbContext _context = context;
    public async Task<UserDto?> CreateAsync(Guid userId, UserRegisterDto userRegisterDto)
    {
        var newEmployer = new Employee
        {
            FullName = userRegisterDto.FullName,
           // PhotoUrl = userRegisterDto.PhotoUrl,
            DateOfBirth = userRegisterDto.DateOfBirth,
            Email = userRegisterDto.Email,
            IsEmailPublic = userRegisterDto.IsEmailPublic,
            PhoneNumber = userRegisterDto.PhoneNumber,
            Telegram = userRegisterDto.Telegram,
            IsTelegramPublic = userRegisterDto.IsTelegramPublic,
            Position = userRegisterDto.Position,
            Department = userRegisterDto.Department,
            HireDate = userRegisterDto.HireDate,
            PassportInfo = userRegisterDto.PassportInfo,
            UserId = userId
        };

        if(userRegisterDto.Role != UserRole.Admin && userRegisterDto.ManagerId.ToString() != "3fa85f64-5717-4562-b3fc-2c963f66afa6")
        {
            var manager = await _context.Employees
                    .FirstOrDefaultAsync(x => x.Id == userRegisterDto.ManagerId && x.User.Role == UserRole.Manager)
                    ?? throw new ApiException("Manager not found");
            newEmployer.ManagerId = manager.ManagerId;
        }

        await _context.Employees.AddAsync(newEmployer);
        
        return newEmployer.Adapt<UserDto>();
    }
}

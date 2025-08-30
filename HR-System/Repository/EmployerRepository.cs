using HR_System.Data;
using HR_System.Entities;
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
        var employer = await _context.Employees.FirstOrDefaultAsync(x => x.UserId == userId);

        if (employer is null)
            return null;

        var newEmployer = new Employee
        {
            FullName = userRegisterDto.FullName,
            PhotoUrl = userRegisterDto.PhotoUrl,
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

        if (userRegisterDto.Role != UserRole.Admin)
        {
            var manager = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == userRegisterDto.ManagerId)
                ?? throw new Exception("Manager not found.");
            newEmployer.ManagerId = manager.Id;
        }

        await _context.Employees.AddAsync(newEmployer);

        return newEmployer.Adapt<UserDto>();
    }
}

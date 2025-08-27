using HR_System.Data;
using HR_System.Entities;
using HR_System.Interfaces.Repository;
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
            return null; // Employer already exists

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
            Department = userRegisterDto.Department
        };
        await _context.Employees.AddAsync(newEmployer);
        return new UserDto
        {
            FullName = newEmployer.FullName,
            Email = newEmployer.Email,
            PhoneNumber = newEmployer.PhoneNumber,
            DateOfBirth = newEmployer.DateOfBirth,
            Position = newEmployer.Position,
            Department = newEmployer.Department,
            HireDate = newEmployer.HireDate
        };
    }
}

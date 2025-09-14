using HR_System.Data;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces.Repository;
using HR_System.Interfaces.Service;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Repository;
public class EmployerRepository(AppDbContext context, IFileService fileService) : IEmployerRepository
{
    private readonly AppDbContext _context = context;
    public async Task<UserDto?> CreateAsync(Employee newEmployer)
    {
        await _context.Employees.AddAsync(newEmployer);
        
        return newEmployer.Adapt<UserDto>();
    }
}

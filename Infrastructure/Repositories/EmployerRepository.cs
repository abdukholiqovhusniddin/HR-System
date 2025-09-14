using Application.DTOs.Responses;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.DataContext;
using Mapster;

namespace Infrastructure.Repositories;
public class EmployerRepository(AppDbContext context, IFileService fileService) : IEmployerRepository
{
    private readonly AppDbContext _context = context;
    public async Task<UserResponseDto?> CreateAsync(Employee newEmployer)
    {
        await _context.Employees.AddAsync(newEmployer);

        return newEmployer.Adapt<UserResponseDto>();
    }
}

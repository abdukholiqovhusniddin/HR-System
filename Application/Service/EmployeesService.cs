using HR_System.DTOs;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces.Repository;
using HR_System.Interfaces.Service;
using Mapster;
using static HR_System.DTOs.EmployeeDto;

namespace HR_System.Service;
public class EmployeesService(IEmployeesRepository directory) : IEmployeesService
{
    private readonly IEmployeesRepository _directoryRepository = directory;

    public async Task<EmployeeCreateDto> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new ApiException("Id is empty");

        var employee = await _directoryRepository.GetById(id);

        return employee is null ? throw new NotFoundException("Employee not found") 
            : employee.Adapt<EmployeeCreateDto>();
    }

    public async Task<List<DirectoryDto>> GetDirectory()
    {
        var directoryDto = await _directoryRepository.GetAllDirectory();

        return [.. directoryDto.Select(a => new DirectoryDto(a.FullName,
            a.Position, a.Department))];
    }
}

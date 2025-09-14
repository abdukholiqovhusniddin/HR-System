using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;

namespace Application.Service;
public class EmployeesService(IEmployeesRepository directory) : IEmployeesService
{
    private readonly IEmployeesRepository _directoryRepository = directory;

    public async Task<EmployeeCreateResponseDto> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new ApiException("Id is empty");

        var employee = await _directoryRepository.GetById(id);

        return employee is null ? throw new NotFoundException("Employee not found")
            : employee.Adapt<EmployeeCreateResponseDto>();
    }

    public async Task<List<DirectoryResponseDto>> GetDirectory()
    {
        var directoryDto = await _directoryRepository.GetAllDirectory();

        return [.. directoryDto.Select(a => new DirectoryResponseDto(a.FullName,
            a.Position, a.Department))];
    }
}

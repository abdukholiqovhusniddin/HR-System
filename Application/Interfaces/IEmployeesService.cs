using Application.Features.Employees.Models;

namespace Application.Interfaces;
public interface IEmployeesService
{
    Task<GetEmployeeDto> GetById(Guid id);
    Task<List<DirectoryResponseDto>> GetDirectory();
}

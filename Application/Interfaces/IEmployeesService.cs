using Application.DTOs.Responses;

namespace Application.Interfaces;
public interface IEmployeesService
{
    Task<EmployeeCreateResponseDto> GetById(Guid id);
    Task<List<DirectoryResponseDto>> GetDirectory();
}

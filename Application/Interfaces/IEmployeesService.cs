namespace Application.Interfaces;
public interface IEmployeesService
{
    Task<EmployeeCreateDto> GetById(Guid id);
    Task<List<DirectoryDto>> GetDirectory();
}

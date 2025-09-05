using HR_System.DTOs;

namespace HR_System.Interfaces.Service;
public interface IFileService
{
    Task RemoveAsync(string fileName);
    Task<FileDto> SaveAsync(IFormFile file);
}

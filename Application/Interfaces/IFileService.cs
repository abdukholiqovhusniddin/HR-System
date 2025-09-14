namespace Application.Interfaces;
public interface IFileService
{
    Task RemoveAsync(string fileName);
    Task<FileDto> SaveAsync(IFormFile file);
}

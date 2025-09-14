using HR_System.DTOs;
using HR_System.Interfaces.Service;

namespace HR_System.Service;
public class FileService(IWebHostEnvironment env) : IFileService
{
    private readonly IWebHostEnvironment _env = env;

    public async Task RemoveAsync(string fileName)
    {
        if(string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

        string filePath = Path.Combine(_env.WebRootPath, fileName.TrimStart('/'));
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }

    public async Task<FileDto> SaveAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }
        string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        string fileExtension = Path.GetExtension(file.FileName);
        string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new FileDto
        {
            Name = file.FileName,
            Url = $"/uploads/{uniqueFileName}",
            Extension = fileExtension,
            Size = file.Length
        };
    }
}

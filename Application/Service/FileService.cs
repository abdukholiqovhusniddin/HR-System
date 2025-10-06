using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Service;
public class FileService(IWebHostEnvironment env) : IFileService
{
    private readonly IWebHostEnvironment _env = env;

    public async Task RemoveAsync(string fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
            throw new ArgumentException("File path cannot be null or empty.", nameof(fileUrl));

        string relativePath = fileUrl.TrimStart('/', '\\');
        string fullPath = Path.Combine(_env.WebRootPath, relativePath);

        if (!fullPath.StartsWith(_env.WebRootPath, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Invalid file path.");

        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath));
        }
        else
        {
            Console.WriteLine($"File not found: {fullPath}");
        }
    }

    public async Task<FileDto> SaveAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }
        string uploadsFolder = Path.Combine(_env.WebRootPath, $"{folderName}");
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
            Url = $"/{folderName}/{uniqueFileName}",
            Extension = fileExtension,
            Size = file.Length
        };
    }
}

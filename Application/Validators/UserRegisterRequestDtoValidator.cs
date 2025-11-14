using Application.DTOs.Employees.Requests;
using Application.DTOs.Users.Requests;
using FluentValidation;

namespace Application.Validators;
public class UserRegisterRequestDtoValidator : AbstractValidator<UserRegisterRequestDto>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];
    private const long MaxFileBytes = 5 * 1024 * 1024; // 5 MB

    public UserRegisterRequestDtoValidator()
    {
        RuleFor(x => x.Photo.Length)
            .LessThanOrEqualTo(MaxFileBytes)
            .WithMessage("Photo size must be 5 MB or less.");

        RuleFor(x => x.Photo.FileName)
            .Must(fn =>
            {
                var ext = Path.GetExtension(fn)?.ToLowerInvariant();
                return ext is not null && AllowedExtensions.Contains(ext);
            })
            .WithMessage("Photo must be a JPG or PNG file.");
        
    }
}

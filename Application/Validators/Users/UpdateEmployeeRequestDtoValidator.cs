using Application.DTOs.Employees.Requests;
using FluentValidation;

namespace Application.Validators.Users;
public class UpdateEmployeeRequestDtoValidator : AbstractValidator<UpdateEmployeeDtoRequest>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];
    private const long MaxFileBytes = 5 * 1024 * 1024; // 5 MB

    public UpdateEmployeeRequestDtoValidator()
    {
        When(x => x.Photo != null, () =>
        {
            RuleFor(x => x.Photo!.Length)
                .LessThanOrEqualTo(MaxFileBytes)
                .WithMessage("Photo size must be 5 MB or less.");

            RuleFor(x => x.Photo!.FileName)
                .Must(fn =>
                {
                    var ext = Path.GetExtension(fn)?.ToLowerInvariant();
                    return ext != null && AllowedExtensions.Contains(ext);
                })
                .WithMessage("Photo must be a JPG or PNG file.");
        });
    }
}

using System;
using System.IO;
using System.Linq;
using FluentValidation;
using Application.DTOs.CommonsDto;

namespace Application.Validators;
public class CreateAndUpdateEmployeeDtoValidator : AbstractValidator<CreateAndUpdateEmployeeDto>
{
    //private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];
    //private const long MaxFileBytes = 5 * 1024 * 1024; // 5 MB

    public CreateAndUpdateEmployeeDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid role.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

        //When(x => x.Photo != null, () =>
        //{
        //    RuleFor(x => x.Photo.Length)
        //        .LessThanOrEqualTo(MaxFileBytes)
        //        .WithMessage("Photo size must be 5 MB or less.");

        //    RuleFor(x => x.Photo.FileName)
        //        .Must(fn =>
        //        {
        //            var ext = Path.GetExtension(fn)?.ToLowerInvariant();
        //            return ext != null && AllowedExtensions.Contains(ext);
        //        })
        //        .WithMessage("Photo must be a JPG or PNG file.");
        //});

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s\-]{7,30}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Telegram)
            .NotEmpty().WithMessage("Telegram is required.")
            .MaximumLength(50).WithMessage("Telegram cannot exceed 50 characters.")
            .Matches(@"^@?[\w\d_]{3,}$").WithMessage("Invalid Telegram handle.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(100);

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required.")
            .MaximumLength(100);

        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Hire date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Hire date cannot be in the future.");

        RuleFor(x => x.PassportInfo)
            .NotEmpty().WithMessage("Passport info is required.")
            .MaximumLength(200).WithMessage("Passport info is too long.");

        When(x => x.ManagerId.HasValue, () =>
        {
            RuleFor(x => x.ManagerId!.Value)
                .NotEqual(Guid.Empty).WithMessage("ManagerId is invalid.");
        });
    }
}
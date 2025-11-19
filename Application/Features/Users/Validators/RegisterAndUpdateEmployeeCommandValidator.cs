using Application.DTOs.CommonsDto;
using FluentValidation;

namespace Application.Features.Users.Validators;
public class RegisterAndUpdateEmployeeCommandValidator : AbstractValidator<CreateAndUpdateEmployeeDto>
{
    public RegisterAndUpdateEmployeeCommandValidator()
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

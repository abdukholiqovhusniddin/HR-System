using Application.Features.Employees.Models;
using Mapster;

namespace Application.Mappers;

public class RegisterMappers : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserProfileResponseDto>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.FullName, src => src.EmployeeProfile.FullName)
            .Map(dest => dest.DateOfBirth, src => src.EmployeeProfile.DateOfBirth)
            .Map(dest => dest.IsEmailPublic, src => src.EmployeeProfile.IsEmailPublic)
            .Map(dest => dest.PhoneNumber, src => src.EmployeeProfile.PhoneNumber)
            .Map(dest => dest.Telegram, src => src.EmployeeProfile.Telegram)
            .Map(dest => dest.IsTelegramPublic, src => src.EmployeeProfile.IsTelegramPublic)
            .Map(dest => dest.Position, src => src.EmployeeProfile.Position)
            .Map(dest => dest.Department, src => src.EmployeeProfile.Department)
            .Map(dest => dest.HireDate, src => src.EmployeeProfile != null ? src.EmployeeProfile.HireDate : DateTime.MinValue)
            .Map(dest => dest.PassportInfo, src => src.EmployeeProfile.PassportInfo)
            .Map(dest => dest.Age, src =>
                DateTime.Today.Year - src.EmployeeProfile.DateOfBirth.Year -
                (src.EmployeeProfile.DateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - src.EmployeeProfile.DateOfBirth.Year)) ? 1 : 0));

        config.NewConfig<Employee, UserProfileResponseDto>()
            .Map(dest => dest.Username, src => src.User.Username)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.IsEmailPublic, src => src.IsEmailPublic)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.Telegram, src => src.Telegram)
            .Map(dest => dest.IsTelegramPublic, src => src.IsTelegramPublic)
            .Map(dest => dest.Position, src => src.Position)
            .Map(dest => dest.Department, src => src.Department)
            .Map(dest => dest.HireDate, src => src.HireDate)
            .Map(dest => dest.PassportInfo, src => src.PassportInfo)
            .Map(dest => dest.PhotoUrl, src => src.PhotoUrl)
            .Map(dest => dest.Role, src => src.User.Role)
            .Map(dest => dest.Age, src =>
                DateTime.Today.Year - src.DateOfBirth.Year -
                (src.DateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - src.DateOfBirth.Year)) ? 1 : 0));

        config.NewConfig<Employee, UserResponseDto>();
        config.NewConfig<Employee, GetEmployeeDto>();
    }
}

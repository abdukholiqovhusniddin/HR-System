using HR_System.Entities;
using Mapster;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Mappers;

public class RegisterMappers : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserProfileDto>()
            .Map(dest => dest.FullName, src => src.EmployeeProfile.FullName)
            .Map(dest => dest.PhotoUrl, src => src.EmployeeProfile.PhotoUrl)
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
        config.NewConfig<Employee, UserDto>()
            .Map(dest => dest.Username, src => src.User.Username)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.Role, src => src.User.Role);
    }
}

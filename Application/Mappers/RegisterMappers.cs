using Application.DTOs.Responses;
using Domain.Entities;
using Mapster;

namespace Application.Mappers;

public class RegisterMappers : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Employee, UserProfileResponseDto>()
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
        config.NewConfig<Employee, EmployeeCreateResponseDto>();
    }
}

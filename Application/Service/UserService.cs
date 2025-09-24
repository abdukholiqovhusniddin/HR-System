using Application.Commons;
using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;
using Application.Exceptions;
using Application.Interfaces;
using Application.JwtAuth;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Mapster;

namespace Application.Service;
public class UserService(IUserRepository userRepository, JwtService jwtService,
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IEmailService emailService,
    IEmployerRepository employerRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployerRepository _employerRepository = employerRepository;

    //public static string GeneratePasswordForUser() =>
    //    PasswordHelper.PasswordGeneration();

    //public async Task<UserResponseDto> CreateUserAsync(UserRegisterRequestDto userRegisterDto)
    //{
    //    //if (await _userRepository.ExistsAsync(userRegisterDto.Username))
    //    //    throw new ApiException("User already exists.");
    //    //if (await _userRepository.ExistsAsync(userRegisterDto.Email))
    //    //    throw new ApiException("Email already exists.");
    //    //if (!Enum.IsDefined(userRegisterDto.Role))
    //    //    throw new ApiException("Invalid role value.");

    //    string password = GeneratePasswordForUser();

    //    var user = new User
    //    {
    //        Username = userRegisterDto.Username,
    //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
    //        Role = userRegisterDto.Role
    //    };

    //    var userId = await _userRepository.CreateAsync(user);

    //    // создаём сущность Employer
    //    var newEmployer = new Employee
    //    {
    //        FullName = userRegisterDto.FullName,
    //        DateOfBirth = userRegisterDto.DateOfBirth,
    //        Email = userRegisterDto.Email,
    //        IsEmailPublic = userRegisterDto.IsEmailPublic,
    //        PhoneNumber = userRegisterDto.PhoneNumber,
    //        Telegram = userRegisterDto.Telegram,
    //        IsTelegramPublic = userRegisterDto.IsTelegramPublic,
    //        Position = userRegisterDto.Position,
    //        Department = userRegisterDto.Department,
    //        HireDate = userRegisterDto.HireDate,
    //        PassportInfo = userRegisterDto.PassportInfo,
    //        UserId = userId
    //    };

    //    // фото загружаем через сервис (Application уровень)
    //    if (userRegisterDto.Photo is not null)
    //    {
    //        var imagePath = await fileService.SaveAsync(userRegisterDto.Photo);

    //        newEmployer.Image = new DataFile(newEmployer.Id,
    //            imagePath.Name,
    //            imagePath.Url,
    //            imagePath.Size,
    //            imagePath.Extension);

    //        newEmployer.PhotoUrl = imagePath.Url;
    //    }

    //    var employee = await _employerRepository.CreateAsync(newEmployer);
    //    var userDto = employee.Adapt<UserResponseDto>();

    //    userDto.Password = password;
    //    userDto.Username = userRegisterDto.Username;
    //    userDto.Email = userRegisterDto.Email;
    //    userDto.Role = userRegisterDto.Role;

    //    await _unitOfWork.SaveChangesAsync(CancellationToken.None);

    //    await emailService.SendPasswordEmailGmailAsync(userRegisterDto.Email, userRegisterDto.Username, password);
    //    return userDto;
    //}

    public async Task<string?> LoginAsync(UserLoginRequestDto userLoginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(userLoginDto.Username);
        if (user == null)
            return null;
        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            return null;
        return jwtService.GenerateToken(user);
    }

    public async Task<UserProfileResponseDto?> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ApiException("Username cannot be null.");

        var user = await _userRepository.GetByUsernameAsync(username, includeEmployeeProfile: true);

       return user?.EmployeeProfile?.Adapt<UserProfileResponseDto>();
    }

    public async Task<UserProfileResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto)
    {
        if (!Enum.IsDefined(dto.Role))
            throw new Exception("Invalid role.");

        var user = await _userRepository.GetByUsernameAsync(dto.Username, includeEmployeeProfile: true);

        if (user == null)
            return null;

        user.Role = dto.Role;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserProfileResponseDto>();
    }
}

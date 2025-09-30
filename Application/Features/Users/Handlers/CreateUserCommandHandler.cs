using Application.Commons;
using Application.DTOs.Users.Responses;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Users.Handlers;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository,
    IUnitOfWork unitOfWork, IFileService fileService,
    IEmailService emailService, IEmployerRepository employerRepository) : IRequestHandler<RegisterUserCommand, ApiResponse<UserResponseDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployerRepository _employerRepository = employerRepository;

    public static string GeneratePasswordForUser() =>
        PasswordHelper.PasswordGeneration();

    public async Task<ApiResponse<UserResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userRegisterDto = request.UserRegisterDto;

        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
            throw new ApiException("User already exists.");

        if (await _userRepository.ExistsAsync(userRegisterDto.Email))
            throw new ApiException("Email already exists.");

        if (!Enum.IsDefined(userRegisterDto.Role))
            throw new ApiException("Invalid role value.");

        string password = GeneratePasswordForUser();

        var user = new User
        {
            Username = userRegisterDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = userRegisterDto.Role
        };

        var userId = await _userRepository.CreateAsync(user);

        // создаём сущность Employer
        var newEmployer = new Employee
        {
            FullName = userRegisterDto.FullName,
            DateOfBirth = userRegisterDto.DateOfBirth,
            Email = userRegisterDto.Email,
            IsEmailPublic = userRegisterDto.IsEmailPublic,
            PhoneNumber = userRegisterDto.PhoneNumber,
            Telegram = userRegisterDto.Telegram,
            IsTelegramPublic = userRegisterDto.IsTelegramPublic,
            Position = userRegisterDto.Position,
            Department = userRegisterDto.Department,
            HireDate = userRegisterDto.HireDate,
            PassportInfo = userRegisterDto.PassportInfo,
            UserId = userId
        };

        // фото загружаем через сервис (Application уровень)
        if (userRegisterDto.Photo is not null)
        {
            var imagePath = await fileService.SaveAsync(userRegisterDto.Photo);

            newEmployer.Image = new EmployeeFile()
            {
                EmployeeId = newEmployer.Id,
                Name = imagePath.Name,
                Url = imagePath.Url,
                Size = imagePath.Size,
                Extension = imagePath.Extension,
            };

            //newEmployer.Image = new EmployeeFile()
            //{
            //    newEmployer.Id,
            //    imagePath.Name,
            //    imagePath.Url,
            //    imagePath.Size,
            //    imagePath.Extension
            //};

            newEmployer.PhotoUrl = imagePath.Url;
        }

        var employee = await _employerRepository.CreateAsync(newEmployer);
        var userDto = employee.Adapt<UserResponseDto>();

        userDto.Password = password;
        userDto.Username = userRegisterDto.Username;
        userDto.Email = userRegisterDto.Email;
        userDto.Role = userRegisterDto.Role;

        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        await emailService.SendPasswordEmailGmailAsync(userRegisterDto.Email, userRegisterDto.Username, password);

        if (userDto is null)
        {
            return new ApiResponse<UserResponseDto>
            {
                Error = "User creation failed.",
                StatusCode = 400
            };
        }
        return new ApiResponse<UserResponseDto>
        {
            Data = userDto,
            StatusCode = 201
        };
    }
}
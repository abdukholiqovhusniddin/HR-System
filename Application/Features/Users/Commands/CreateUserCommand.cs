using Application.Commons;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands;
public sealed class CreateUserCommand(UserRegisterRequestDto userRegisterDto) : IRequest<ApiResponse<UserResponseDto>>;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository,
    IUnitOfWork unitOfWork, IFileService fileService,
    IEmailService emailService, IEmployerRepository employerRepository) : IRequestHandler<CreateUserCommand, ApiResponse<UserResponseDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployerRepository _employerRepository = employerRepository;
    public async Task<ApiResponse<UserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //var createdUser = await _userService.CreateUserAsync(request.userRegisterDto);
        //if (createdUser == null)
        //{
        //    return new ApiResponse<UserResponseDto>
        //    {
        //        Error = "User creation failed. User or email already exists.",
        //        StatusCode = 400
        //    };
        //}
        //return new ApiResponse<UserResponseDto>
        //{
        //    Data = createdUser,
        //    StatusCode = 201
        //};
    }
}
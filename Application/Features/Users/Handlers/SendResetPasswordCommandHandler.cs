using Application.Commons;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Handlers;
internal class SendResetPasswordCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailService emailService) 
    : IRequestHandler<SendResetPasswordCommand, ApiResponse<string>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public static string GenerateResetPassword() =>
         PasswordHelper.PasswordGeneration();

    public async Task<ApiResponse<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Guid userId = request.requestDto.UserId;
        string newPassword = GenerateResetPassword();

        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new ApiException("User not found.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await emailService.SendPasswordEmailGmailAsync(user.EmployeeProfile!.Email, user.Username!, newPassword);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>
        {
            Data = $"Reset password email sent successfully. {user.Username} password: {newPassword}",
            StatusCode = 200
        };
    }
}

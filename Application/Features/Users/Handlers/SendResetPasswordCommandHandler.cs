using Application.Commons;
using Application.Features.Users.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Handlers;
internal class SendResetPasswordCommandHandler(IUserRepository userRepository) 
    : IRequestHandler<SendResetPasswordCommand, ApiResponse<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public Task<ApiResponse<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var requestDto = request.requestDto;
        // Логика отправки письма для сброса пароля
        // Например, генерация токена и отправка email
        return Task.FromResult(new ApiResponse<string>
        {
            Data = "Reset password email sent successfully.",
            StatusCode = 200
        });
    }
}

using Application.Commons;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.JwtAuth;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Handlers;
internal sealed class LoginUserCommandHandler(IUserRepository userRepository,
    JwtService jwtService) : IRequestHandler<LoginUserCommand, ApiResponse<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<ApiResponse<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loginDto = request.UserLoginDto;
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new ApiException("Invalid username or password.");
        }
        var token = jwtService.GenerateToken(user);
        return new ApiResponse<string>(token);
    }
}
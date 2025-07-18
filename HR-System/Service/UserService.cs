using HR_System.DTOs;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Service;
public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
        {
            throw new ApiException("User already exists.");
        }

        await _userRepository.CreateAsync(new User
        {
            Username = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
            Role = userRegisterDto.Role
        });
        return new UserDto(userRegisterDto.Username, userRegisterDto.Email, userRegisterDto.Role);
    }

    public Task<string?> LoginAsync(UserAuthDto.LoginResponseDto userLoginDto)
    {
        throw new NotImplementedException();
    }
}

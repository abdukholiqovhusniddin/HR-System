using HR_System.DTOs;
using HR_System.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateUser(UserRegisterDto userRegisterDto)
    {
        UserDto createdUser = await _service.CreateUserAsync(userRegisterDto);
        if (createdUser == null)
        {
            return BadRequest((new ApiResponse<object>
            {
                Error = "User with this email already exists.",
                StatusCode = 400
            }));
        }
        return Ok(new ApiResponse<object>
        {
            Data = createdUser,
            StatusCode = 200
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var token = await _service.LoginAsync(userLoginDto);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized((new ApiResponse<object>
            {
                Error = "Invalid email or password.",
                StatusCode = 401
            }));
        }
        return Ok((new ApiResponse<object>
        {
            Data = token,
            StatusCode = 200
        }));
    }
}
using System.Security.Claims;
using HR_System.DTOs;
using HR_System.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [Authorize(Roles = "Admin")]
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
                Error = "Invalid username or password.",
                StatusCode = 401
            }));
        }
        return Ok((new ApiResponse<object>
        {
            Data = token,
            StatusCode = 200
        }));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            return Unauthorized();

        var user = await _service.GetByUsernameAsync(username, role);

        if (user == null)
        {
            return NotFound((new ApiResponse<object>
            {
                Error = "User not found",
                StatusCode = 404
            }));
        }
        return Ok((new ApiResponse<object>
        {
            Data = user,
            StatusCode = 200
        }));
    }
}
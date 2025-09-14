using HR_System.Commons;
using HR_System.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HR_System.DTOs.UserAuthDto;

namespace HR_System.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : ApiControllerBase
{
    private readonly IUserService _service = service; // DI- Dependency Injection   

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateUser([FromForm]UserRegisterDto userRegisterDto)
    {
        UserDto createdUser = await _service.CreateUserAsync(userRegisterDto);
        if (createdUser == null)
        {
            return BadRequest((new ApiResponse<object>
            {
                Error = "User creation failed. User or email already exists.",
                StatusCode = 400
            }));
        }
        return Ok(new ApiResponse<object>
        {
            Data = createdUser,
            StatusCode = 201
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
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
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken = default)
    {
        if (!IsAuthenticated)
            return Unauthorized((new ApiResponse<object>
            {
                Error = "User is not authenticated.",
                StatusCode = 401
            }));
        UserProfileDto? user = await _service.GetByUsernameAsync(UserName);

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

    [Authorize(Roles = "Admin")]
    [HttpPut("assign-role")]
    public async Task<ActionResult<UserProfileDto>> AssignRole([FromBody] AssignRoleDto dto)
    {
        UserProfileDto? updatedUser = await _service.AssignRoleAsync(dto);

        if (updatedUser == null)
            return NotFound("User not found");

        return Ok(updatedUser);
    }
}
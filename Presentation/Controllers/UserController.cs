using System.Threading;
using Application.Commons;
using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;
using Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator; // DI- Dependency Injection   

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateUser([FromForm] UserRegisterRequestDto userRegisterDto, CancellationToken cancellationToken)
    {
        var responseUserRegister = await _mediator.Send(new CreateUserCommand(userRegisterDto), cancellationToken);
        //if (createdUser == null)
        //{
        //    return BadRequest(new ApiResponse<object>
        //    {
        //        Error = "User creation failed. User or email already exists.",
        //        StatusCode = 400
        //    });
        //}
        //return Ok(new ApiResponse<object>
        //{
        //    Data = createdUser,
        //    StatusCode = 201
        //});
        return StatusCode(responseUserRegister.StatusCode, responseUserRegister);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginDto)
    {
        var token = await _service.LoginAsync(userLoginDto);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new ApiResponse<object>
            {
                Error = "Invalid username or password.",
                StatusCode = 401
            });
        }
        return Ok(new ApiResponse<object>
        {
            Data = token,
            StatusCode = 200
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken = default)
    {
        if (!IsAuthenticated)
            return Unauthorized(new ApiResponse<object>
            {
                Error = "User is not authenticated.",
                StatusCode = 401
            });
        UserProfileResponseDto? user = await _service.GetByUsernameAsync(UserName);

        if (user == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Error = "User not found",
                StatusCode = 404
            });
        }
        return Ok(new ApiResponse<object>
        {
            Data = user,
            StatusCode = 200
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("assign-role")]
    public async Task<ActionResult<UserProfileResponseDto>> AssignRole([FromBody] AssignRoleRequestDto dto)
    {
        UserProfileResponseDto? updatedUser = await _service.AssignRoleAsync(dto);

        if (updatedUser == null)
            return NotFound("User not found");

        return Ok(updatedUser);
    }
}
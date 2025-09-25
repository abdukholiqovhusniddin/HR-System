using System.Threading;
using Application.Commons;
using Application.DTOs.Employees.Requests;
using Application.DTOs.Employees.Responses;
using Application.DTOs.Users.Requests;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
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
    public async Task<IActionResult> CreateUser([FromForm] UserRegisterRequestDto userRegisterDto)
    {
        var responseUserRegister = await _mediator.Send(new RegisterUserCommand(userRegisterDto));

        return StatusCode(responseUserRegister.StatusCode, responseUserRegister);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginDto)
    {
        var token = await _mediator.Send(new LoginUserCommand(userLoginDto));

        return StatusCode(token.StatusCode, token);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMeQuery()
    {
        if (!IsAuthenticated)
            return Unauthorized(new ApiResponse<string>
            {
                Error = "User is not authenticated.",
                StatusCode = 401
            });

        var user = await _mediator.Send(new GetMeQuery(UserName));

        //if (user == null)
        //{
        //    return NotFound(new ApiResponse<object>
        //    {
        //        Error = "User not found",
        //        StatusCode = 404
        //    });
        //}
        //return Ok(new ApiResponse<object>
        //{
        //    Data = user,
        //    StatusCode = 200
        //});
        return StatusCode(user.StatusCode, user);
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
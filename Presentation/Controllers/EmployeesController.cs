using Application.Commons;
using Application.DTOs.Employees.Requests;
using Application.DTOs.Users.Requests;
using Application.Features.Employees.Commands;
using Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController(IMediator mediator) : ApiControllerBase
{

    [HttpGet]
    [Route("directory")]
    public async Task<IActionResult> GetDirectory()
    {
        var directoryDto = await mediator.Send(new GetEmployeeDirectory());
        return StatusCode(directoryDto.StatusCode, directoryDto);
    }

    [Authorize(Roles = "Admin, HR")]
    [HttpGet("Get by Id")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var employeeDto = await mediator.Send(new GetEmployeeById(Id));
        return StatusCode(employeeDto.StatusCode, employeeDto);
    }

    [Authorize(Roles = "Admin, HR")]
    [HttpDelete("Delete by Id")]
    public async Task<IActionResult> DeleteById(Guid Id)
    {
        var response = await mediator.Send(new DeleteEmployeeCommand(Id));
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "HR")]
    [HttpPost]
    public async Task<IActionResult> UpdateEmployee([FromForm] UpdateEmployeeDtoRequest command)
    {
        var response = await mediator.Send(new UpdateEmployeeCommand(command));
        return StatusCode(response.StatusCode, response);
    }
}

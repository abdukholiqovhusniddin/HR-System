using Application.Commons;
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
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid Id)
    {
        var employeeDto = await mediator.Send(new GetEmployeeById(Id));

        return StatusCode(employeeDto.StatusCode, employeeDto);
    }

}

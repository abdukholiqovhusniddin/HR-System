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
    private readonly IMediator _mediator = mediator; // DI- Dependency Injection

    [HttpGet]
    [Route("directory")]
    public async Task<IActionResult> GetDirectory(CancellationToken cancellationToken)
    {
        var directoryDto = await _mediator.Send(new GetEmployeeDirectory(), cancellationToken);

        return StatusCode(directoryDto.StatusCode, directoryDto);
    }

    [Authorize(Roles = "Admin, HR")]
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid Id, CancellationToken cancellationToken)
    {
        var employeeDto = await _mediator.Send(new GetEmployeeById(Id), cancellationToken);

        return StatusCode(employeeDto.StatusCode, employeeDto);
    }

}

using Application.DTOs.Vacations.Requests;
using Application.Features.Vacations.Commands;
using Application.Features.Vacations.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationsController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// GET /api/vacations/me
    [HttpGet("me")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> GetMyVacations()
    {
        var vacations = await _mediator.Send(new GetMyVacationsQuery(UserId));
        return StatusCode(vacations.StatusCode, vacations);
    }

    /// POST /api/vacations
    [HttpPost("")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> CreateVacation(CreateVacationDtoRequest createVacationDtoRequest)
    {
        var command = await _mediator.Send(new CreateVacationCommand(UserId, createVacationDtoRequest));
        return StatusCode(command.StatusCode, command);
    }

}

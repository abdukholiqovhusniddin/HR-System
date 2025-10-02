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

    [HttpGet("me")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> GetMyVacations()
    {
        var vacations = await _mediator.Send(new GetMyVacationsQuery(UserId));
        return StatusCode(vacations.StatusCode, vacations);
    }
}

using Application.Features.Equipment.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "HR")]
public class EquipmentsController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// Get equipment assigned to an employee
    [HttpGet("${employeeId}")]
    public async Task<IActionResult> GetByEmployeeId(Guid employeeId)
    {
        var result = await _mediator.Send(new GetEquipmentByEmployeeIdQuery(employeeId));
        return StatusCode(result.StatusCode, result);
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "HR")]
public class ContractsController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("employeeId")]
    public async Task<IActionResult> GetById(Guid employeeId)
    {
        var employeeContract = await _mediator.Send(new GetContractById(employeeId));

        return StatusCode(employeeContract.StatusCode, employeeContract);
    }

}

using Application.Features.Salaries.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalariesController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("Get salary by employeeId")]
    [Authorize(Roles = "HR, Accountant")]
    public async Task<IActionResult> GetSalaryByEmployeeId(Guid employeeId)
    {
        var salaryDto = await _mediator.Send(new GetSalaryByIdQuery(employeeId));
        return StatusCode(salaryDto.StatusCode, salaryDto);
    }

}

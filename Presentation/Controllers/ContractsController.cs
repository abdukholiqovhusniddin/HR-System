using Application.DTOs.Contract.Requests;
using Application.Features.Contracts.Commands;
using Application.Features.Contracts.Queries;
using Domain.Entities;
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
        var employeeContract = await _mediator.Send(new GetContractQuery(employeeId));

        return StatusCode(employeeContract.StatusCode, employeeContract);
    }

    [HttpPost("Add contract")]
    public async Task<IActionResult> AddContract(AddContractDtoRequest employeeContract)
    {
        var result = await _mediator.Send(new AddContractCommand(employeeContract));

        return StatusCode(result.StatusCode, result);
    }
}

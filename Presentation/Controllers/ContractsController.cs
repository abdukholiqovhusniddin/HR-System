using Application.DTOs.Contract.Requests;
using Application.Features.Contracts.Commands;
using Application.Features.Contracts.Queries;
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

    // Get contracts for an employee
    [HttpGet("${employeeId}")]
    public async Task<IActionResult> GetByEmployeeId(Guid employeeId)
    {
        var employeeContract = await _mediator.Send(new GetContractQuery(employeeId));

        return StatusCode(employeeContract.StatusCode, employeeContract);
    }

    // Add a new contract
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddContractDtoRequest employeeContract)
    {
        var result = await _mediator.Send(new AddContractCommand(employeeContract));

        return StatusCode(result.StatusCode, result);
    }

    // Update a contract
    [HttpPut("updateContract")]
    public async Task<IActionResult> UpdateContract([FromForm] UpdateContractDtoRequest updateContract)
    {
        var respons = await _mediator.Send(new UpdateContractCommon(updateContract));

        return StatusCode(respons.StatusCode, respons);
    }

    // Delete a contract
    [HttpDelete("${contractId}")]
    public async Task<IActionResult> DeleteContract(Guid contractId)
    {
        var response = await _mediator.Send(new DeleteContractCommand(contractId));
        return StatusCode(response.StatusCode, response);
    }
}
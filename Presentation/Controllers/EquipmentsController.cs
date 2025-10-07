using Application.DTOs.Equipments.Requests;
using Application.Features.Equipment.Commands;
using Application.Features.Equipment.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    /// Add new equipment for employee
    [HttpPost]
    public async Task<IActionResult> AddEquipment([FromForm]AddEquipmentDtoRequest addEquipmentDtoRequest)
    {
        var result = await _mediator.Send(new AddEquipmentCommand(addEquipmentDtoRequest));
        return StatusCode(result.StatusCode, result);
    }

    /// Update equipment status (e.g. Returned)
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, EquipmentStatus status)
    {
        var result = await _mediator.Send(new UpdateEquipmentStatusCommand(id, status));
        return StatusCode(result.StatusCode, result);
    }
}

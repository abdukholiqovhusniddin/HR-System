using Application.DTOs.Salaries.Requests;
using Application.Features.Salaries.Commands;
using Application.Features.Salaries.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Accountant")]
public class SalariesController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    // Get salary for an employee
    [HttpGet("{employeeId}")]
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> GetByEmployeeId(Guid employeeId)
    {
        var salaryDto = await _mediator.Send(new GetSalaryByIdQuery(employeeId));
        return StatusCode(salaryDto.StatusCode, salaryDto);
    }

    // Add a new salary
    [HttpPost]
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> Create(AddSalaryDtoRequest request)
    {
        var salaryDto = await _mediator.Send(new CreateSalaryCommand(request));
        return StatusCode(salaryDto.StatusCode, salaryDto);
    }

    // Get salary history for an employee
    [HttpGet("{employeeId}/history")]
    public async Task<IActionResult> GetHistory(Guid employeeId)
    {
        var salaryEmployee = await _mediator.Send(new GetSalaryHistoryQuery(employeeId));
        return StatusCode(salaryEmployee.StatusCode, salaryEmployee);
    }

    // Get salary report for all employees
    [HttpGet("report")]
    public async Task<IActionResult> GetReport()
    {
        var report = await _mediator.Send(new GetSalaryReportQuery());
        return StatusCode(report.StatusCode, report);
    }
}

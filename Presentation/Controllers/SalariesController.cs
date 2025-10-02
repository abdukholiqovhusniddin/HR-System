using Application.DTOs.Salaries.Requests;
using Application.Features.Salaries.Commands;
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

    [HttpPost("Create salary for employee")]
    [Authorize(Roles = "HR, Accountant")]
    public async Task<IActionResult> CreateSalaries(AddSalaryDtoRequest request)
    {
        var salaryDto = await _mediator.Send(new CreateSalaryCommand(request));
        return StatusCode(salaryDto.StatusCode, salaryDto);
    }

    [HttpGet("Get history salary employee by Id")]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> GetSalaryEmployee(Guid salaryEmployeeId)
    {
        var salaryEmployee = await _mediator.Send(new GetSalaryEmployeeQuery(salaryEmployeeId));
        return StatusCode(salaryEmployee.StatusCode, salaryEmployee);
    }

}

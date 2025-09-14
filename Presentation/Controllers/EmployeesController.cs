using HR_System.Commons;
using HR_System.DTOs;
using HR_System.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController(IEmployeesService service) : ApiControllerBase
{
    private readonly IEmployeesService _service = service; // DI- Dependency Injection

    [HttpGet]
    [Route("directory")]
    public async Task<IActionResult> GetDirectory()
    {
        var directoryDto = await _service.GetDirectory();
        return Ok(new ApiResponse<object>
        {
            Data = directoryDto,
            StatusCode = 200
        });
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var employeeDto = await _service.GetById(id);
        return Ok(new ApiResponse<object>
        {
            Data = employeeDto,
            StatusCode = 200
        });
    }

}

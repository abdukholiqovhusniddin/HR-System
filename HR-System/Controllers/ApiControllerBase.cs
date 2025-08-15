using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    protected string UserName => User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    protected string UserRole => User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

    protected bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;
}
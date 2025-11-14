using Application.DTOs.CommonsDto;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Employees.Requests;
public class UpdateEmployeeDtoRequest : CreateAndUpdateEmployeeDto
{
    public required Guid Id { get; set; }
    public IFormFile? Photo { get; set; }
}
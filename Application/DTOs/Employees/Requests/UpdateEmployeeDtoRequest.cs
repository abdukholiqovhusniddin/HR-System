using Application.DTOs.CommonsDto;

namespace Application.DTOs.Employees.Requests;
public class UpdateEmployeeDtoRequest : CreateAndUpdateEmployeeDto
{
    public required Guid Id { get; set; }
}
using Application.DTOs.CommonsDto;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Contract.Requests;
public class AddContractDtoRequest : AddAndUpdateContractDto
{
    public required Guid EmployeeId { get; set; }
    public required IFormFile DocumentPdf { get; set; }
}
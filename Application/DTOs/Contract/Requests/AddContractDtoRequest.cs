using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Contract.Requests;
public class AddContractDtoRequest
{
    public required Guid EmployeeId { get; set; }
    public required ContractType ContractType { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    public string? Terms { get; set; }

    public IFormFile? DocumentPdf { get; set; }
    //public string? DocumentName { get; set; }
    //public string? DocumentType { get; set; }
}

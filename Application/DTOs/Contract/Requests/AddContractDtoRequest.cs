using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Contract.Requests;
public class AddContractDtoRequest
{
    public required Guid EmployeeId { get; set; }
    public required ContractType ContractType { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }

    [StringLength(1000, ErrorMessage = "Terms cannot exceed 1000 characters.")]
    public string? Terms { get; set; }

    public IFormFile? DocumentPdf { get; set; }
}

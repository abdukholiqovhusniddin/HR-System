using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.CommonsDto;
public abstract class AddAndUpdateContractDto
{
    public required ContractType ContractType { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }

    [StringLength(1000, ErrorMessage = "Terms cannot exceed 1000 characters.")]
    public string? Terms { get; set; }
}
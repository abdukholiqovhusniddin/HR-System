using Domain.Enums;

namespace Application.DTOs.Contract.Responses;
public class ContractDtoResponse
{
    public required ContractType ContractType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Terms { get; set; } // Условия контракта

    public string? DocumentUrl { get; set; }

    public bool IsAktive { get; set; }
}

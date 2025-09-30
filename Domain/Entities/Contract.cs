
using Domain.Commons;
using Domain.Enums;

namespace Domain.Entities;
public class Contract: EmployeeBaseEntity
{
    public required ContractType ContractType { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    public string? Terms { get; set; } // Условия контракта

    public string? DocumentUrl { get; set; }

    public ContractFile DocumentPdf { get; set; } = default!;

    public bool IsDeleted { get; set; } = false;
}

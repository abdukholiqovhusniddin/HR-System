using Domain.Commons;

namespace Domain.Entities;
public class ContractFile: DataFile
{
    public required Guid ContractId { get; set; }
    public Contract Contract { get; set; } = default!;
}
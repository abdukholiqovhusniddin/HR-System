namespace Domain.Entities;
public class Contract: EmployeeBaseEntity
{
    public string? ContractType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Terms { get; set; }

    public string? DocumentUrl { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentType { get; set; }
}

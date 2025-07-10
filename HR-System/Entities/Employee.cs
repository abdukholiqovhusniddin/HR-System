using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace HR_System.Entities;
public class Employee : BaseEntity
{
    public string? FullName { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string? Email { get; set; }
    public bool IsEmailPublic { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Telegram { get; set; }
    public bool IsTelegramPublic { get; set; }

    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime HireDate { get; set; }

    public string? PassportInfo { get; set; }

    public int? ManagerId { get; set; }
    public Employee? Manager { get; set; }

    public ICollection<Contract>? Contracts { get; set; }
    public ICollection<Salary>? Salaries { get; set; }
    public ICollection<VacationRequest>? Vacations { get; set; }
    public ICollection<EquipmentAssignment>? Equipments { get; set; }

    [NotMapped]
    public int Age => DateTime.Today.Year - DateOfBirth.Year - 
        (DateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - DateOfBirth.Year)) ? 1 : 0);
}
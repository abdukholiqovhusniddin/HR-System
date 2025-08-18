using HR_System.Entities;

namespace HR_System.Commons;

public static class FilterDataUserExtension
{
    public static List<VacationRequest>? FilterVacations(ICollection<VacationRequest>? src) =>
            src?.Select(v => new VacationRequest
            {
                Id = v.Id,
                CreatedAt = v.CreatedAt,
                VacationType = v.VacationType,
                StartDate = v.StartDate,
                EndDate = v.EndDate,
                Status = v.Status,
                EmployeeId = v.EmployeeId
            }).ToList();
    public static Employee FilterSubordinate(Employee s, bool withContacts, bool withVacations) => new()
    {
        Id = s.Id,
        FullName = s.FullName,
        PhotoUrl = s.PhotoUrl,
        Position = s.Position,
        Department = s.Department,
        DateOfBirth = s.DateOfBirth,
        Email = withContacts && s.IsEmailPublic ? s.Email : null,
        PhoneNumber = withContacts && s.IsEmailPublic ? s.PhoneNumber : null,
        Telegram = withContacts && s.IsTelegramPublic ? s.Telegram : null,
        IsEmailPublic = s.IsEmailPublic,
        IsTelegramPublic = s.IsTelegramPublic,
        Vacations = withVacations
                ? s.Vacations?.OrderByDescending(v => v.EndDate).Take(1).ToList()
                : null
    };
    public static List<Salary>? FilterSalaries(ICollection<Salary>? src) =>
           src?.Select(s => new Salary
           {
               Id = s.Id,
               BaseSalary = s.BaseSalary,
               Bonus = s.Bonus,
               Deduction = s.Deduction,
               StartPeriod = s.StartPeriod,
               EndPeriod = s.EndPeriod,
               EmployeeId = s.EmployeeId
           }).ToList();
    public static Employee? FilterManager(Employee? m) =>
            m == null ? null : new Employee { Id = m.Id, FullName = m.FullName, Position = m.Position };
    
    public static List<Contract>? FilterContracts(ICollection<Contract>? src) =>
            src?.Select(c => new Contract
            {
                Id = c.Id,
                ContractType = c.ContractType,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Terms = c.Terms,
                DocumentUrl = c.DocumentUrl,
                DocumentName = c.DocumentName,
                DocumentType = c.DocumentType,
                EmployeeId = c.EmployeeId
            }).ToList();
    public static List<EquipmentAssignment>? FilterEquipments(ICollection<EquipmentAssignment>? src) =>
            src?.Select(e => new EquipmentAssignment
            {
                Id = e.Id,
                Type = e.Type,
                Model = e.Model,
                InventoryNumber = e.InventoryNumber,
                AssignmentDate = e.AssignmentDate,
                Status = e.Status,
                EmployeeId = e.EmployeeId
            }).ToList();
}

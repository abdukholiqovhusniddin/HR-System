using HR_System.Entities;

namespace HR_System.Commons;

public static class FilterDataUserExtension
{
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
}

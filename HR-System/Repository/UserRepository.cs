using HR_System.Commons;
using HR_System.Data;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repository;
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task CreateAsync(User user)
    {
        if (user == null)
            throw new ApiException("User cannot be null.");
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string? usernameOrEmail)
    {
        if (string.IsNullOrWhiteSpace(usernameOrEmail))
            throw new ApiException("Username or email cannot be null or empty.");
        return await _context.Users.AnyAsync(n => n.Username == usernameOrEmail || n.Email == usernameOrEmail);
    }

    public async Task<User?> GetByUsernameAsync(string? username) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(n => n.Username == username);


    public async Task<User?> GetUserInfoByRoleAsync(Guid userId, string role)
    {
        var user = await _context.Users
            .Include(u => u.EmployeeProfile!)
                .ThenInclude(e => e.Manager)
            .Include(u => u.EmployeeProfile!)
                .ThenInclude(e => e.Subordinates)
            .Include(u => u.EmployeeProfile!)
                .ThenInclude(e => e.Contracts)
            .Include(u => u.EmployeeProfile!)
                .ThenInclude(e => e.Salaries)
            .Include(u => u.EmployeeProfile!)
                .ThenInclude(e => e.Vacations)
            .Include(u => u.EmployeeProfile!)
                .ThenInclude(e => e.Equipments)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        var employee = user.EmployeeProfile;
        if (employee == null)
            return new User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                EmployeeProfile = null
            };

        Employee? FilterManager(Employee? m) =>
            m == null ? null : new Employee { Id = m.Id, FullName = m.FullName, Position = m.Position };

        Employee FilterSubordinate(Employee s, bool withContacts, bool withVacations) => new()
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

        List<Contract>? FilterContracts(ICollection<Contract>? src) =>
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

        

        List<VacationRequest>? FilterVacations(ICollection<VacationRequest>? src) =>
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

        List<EquipmentAssignment>? FilterEquipments(ICollection<EquipmentAssignment>? src) =>
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

        var filteredEmployee = new Employee
        {
            Id = employee.Id,
            FullName = employee.FullName,
            PhotoUrl = employee.PhotoUrl,
            Position = employee.Position,
            Department = employee.Department,
            DateOfBirth = employee.DateOfBirth,
            HireDate = employee.HireDate,
            UserId = employee.UserId,
            ManagerId = employee.ManagerId
        };

        switch (role)
        {
            case "Employee":
                filteredEmployee.Email = employee.IsEmailPublic ? employee.Email : null;
                filteredEmployee.Telegram = employee.IsTelegramPublic ? employee.Telegram : null;
                filteredEmployee.IsEmailPublic = employee.IsEmailPublic;
                filteredEmployee.IsTelegramPublic = employee.IsTelegramPublic;
                filteredEmployee.Vacations = FilterVacations(employee.Vacations);
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => FilterSubordinate(s, true, true)).ToList();
                break;

            case "HR":
                filteredEmployee.Email = employee.Email;
                filteredEmployee.PhoneNumber = employee.PhoneNumber;
                filteredEmployee.Telegram = employee.Telegram;
                filteredEmployee.PassportInfo = employee.PassportInfo;
                filteredEmployee.Manager = FilterManager(employee.Manager);
                filteredEmployee.Contracts = FilterContracts(employee.Contracts);
                filteredEmployee.Salaries = FilterDataUserExtension.FilterSalaries(employee.Salaries);
                filteredEmployee.Vacations = FilterVacations(employee.Vacations);
                filteredEmployee.Equipments = FilterEquipments(employee.Equipments);
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => FilterSubordinate(s, false, false)).ToList();
                break;

            case "Accountant":
                filteredEmployee.Salaries = FilterDataUserExtension.FilterSalaries(employee.Salaries);
                break;

            case "Manager":
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s =>
                {
                    var sub = FilterSubordinate(s, true, false);
                    sub.Vacations = FilterVacations(s.Vacations);
                    return sub;
                }).ToList();
                break;

            case "Admin":
                filteredEmployee.Email = employee.Email;
                filteredEmployee.PhoneNumber = employee.PhoneNumber;
                filteredEmployee.Telegram = employee.Telegram;
                filteredEmployee.PassportInfo = employee.PassportInfo;
                filteredEmployee.Manager = FilterManager(employee.Manager);
                filteredEmployee.Contracts = FilterContracts(employee.Contracts);
                filteredEmployee.Salaries =FilterDataUserExtension.FilterSalaries(employee.Salaries);
                filteredEmployee.Vacations = FilterVacations(employee.Vacations);
                filteredEmployee.Equipments = FilterEquipments(employee.Equipments);
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => FilterSubordinate(s, false, false)).ToList();
                break;

            default:
                throw new ApiException("Invalid role specified.");
        }

        return new User
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            EmployeeProfile = filteredEmployee
        };
    }
}
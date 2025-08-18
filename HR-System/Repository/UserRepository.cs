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


    //public async Task<User?> GetUserInfoByRoleAsync(Guid userId, string role)
    //{
    //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    //    //.Include(u => u.EmployeeProfile!)
    //    //    .ThenInclude(e => e.Manager)
    //    //.Include(u => u.EmployeeProfile!)
    //    //    .ThenInclude(e => e.Subordinates)
    //    //.Include(u => u.EmployeeProfile!)
    //    //    .ThenInclude(e => e.Contracts)
    //    //.Include(u => u.EmployeeProfile!)
    //    //    .ThenInclude(e => e.Salaries)
    //    //.Include(u => u.EmployeeProfile!)
    //    //    .ThenInclude(e => e.Vacations)
    //    //.Include(u => u.EmployeeProfile!)
    //    //    .ThenInclude(e => e.Equipments)


    //    if (user == null)
    //        return null;

    //    var employee = user.EmployeeProfile;

    //    if (employee == null)
    //        return new User
    //        {
    //            Id = user.Id,
    //            Username = user.Username,
    //            Email = user.Email,
    //            Role = user.Role,
    //            CreatedAt = user.CreatedAt,
    //            EmployeeProfile = null
    //        };

    //    var filteredEmployee = new Employee
    //    {
    //        Id = employee.Id,
    //        FullName = employee.FullName,
    //        Email = employee.Email,
    //        PhotoUrl = employee.PhotoUrl,
    //        PhoneNumber = employee.PhoneNumber,
    //        Telegram = employee.Telegram,
    //        IsEmailPublic = employee.IsEmailPublic,
    //        IsTelegramPublic = employee.IsTelegramPublic,
    //        Position = employee.Position,
    //        PassportInfo = employee.PassportInfo,
    //        Department = employee.Department,
    //        DateOfBirth = employee.DateOfBirth,
    //        HireDate = employee.HireDate,
    //        UserId = employee.UserId
    //    };

    //    return new User
    //    {
    //        Id = user.Id,
    //        Username = user.Username,
    //        Email = user.Email,
    //        Role = user.Role,
    //        CreatedAt = user.CreatedAt,
    //        EmployeeProfile = filteredEmployee
    //    };

    //    //filteredEmployee.Vacations = FilterDataUserExtension.FilterVacations(employee.Vacations);

    //    //switch (role)
    //    //{
    //    //    case "Employee":
    //    //        filteredEmployee.Subordinates = employee.Subordinates?.Select(s => FilterDataUserExtension.FilterSubordinate(s, true, true)).ToList();
    //    //        break;

    //    //    case "HR":
    //    //        filteredEmployee.Manager = FilterDataUserExtension.FilterManager(employee.Manager);
    //    //        filteredEmployee.Contracts = FilterDataUserExtension.FilterContracts(employee.Contracts);
    //    //        filteredEmployee.Salaries = FilterDataUserExtension.FilterSalaries(employee.Salaries);
    //    //        filteredEmployee.Equipments = FilterDataUserExtension.FilterEquipments(employee.Equipments);
    //    //        filteredEmployee.Subordinates = employee.Subordinates?.Select(s => FilterDataUserExtension.FilterSubordinate(s, false, false)).ToList();
    //    //        break;

    //    //    case "Accountant":
    //    //        filteredEmployee.Salaries = FilterDataUserExtension.FilterSalaries(employee.Salaries);
    //    //        break;

    //    //    case "Manager":
    //    //        filteredEmployee.Subordinates = employee.Subordinates?.Select(s =>
    //    //        {
    //    //            var sub = FilterDataUserExtension.FilterSubordinate(s, true, false);
    //    //            sub.Vacations = FilterDataUserExtension.FilterVacations(s.Vacations);
    //    //            return sub;
    //    //        }).ToList();
    //    //        break;

    //    //    case "Admin":
    //    //        filteredEmployee.Manager = FilterDataUserExtension.FilterManager(employee.Manager);
    //    //        filteredEmployee.Contracts = FilterDataUserExtension.FilterContracts(employee.Contracts);
    //    //        filteredEmployee.Salaries = FilterDataUserExtension.FilterSalaries(employee.Salaries);
    //    //        filteredEmployee.Equipments = FilterDataUserExtension.FilterEquipments(employee.Equipments);
    //    //        filteredEmployee.Subordinates = employee.Subordinates?.Select(s => FilterDataUserExtension.FilterSubordinate(s, false, false)).ToList();
    //    //        break;

    //    //    default:
    //    //        throw new ApiException("Invalid role specified.");
    //    //}
    //}
}
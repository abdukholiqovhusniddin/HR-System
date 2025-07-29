using System.Linq;
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
            .Include(u => u.EmployeeProfile)
                .ThenInclude(e => e.Manager)
            .Include(u => u.EmployeeProfile)
                .ThenInclude(e => e.Subordinates)
            .Include(u => u.EmployeeProfile)
                .ThenInclude(e => e.Contracts)
            .Include(u => u.EmployeeProfile)
                .ThenInclude(e => e.Salaries)
            .Include(u => u.EmployeeProfile)
                .ThenInclude(e => e.Vacations)
            .Include(u => u.EmployeeProfile)
                .ThenInclude(e => e.Equipments)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        // Deep copy qilish uchun yangi obyektlar yaratamiz
        var result = new User
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            EmployeeProfile = null
        };

        var employee = user.EmployeeProfile;
        if (employee == null)
            return result;

        var filteredEmployee = new Employee
        {
            Id = employee.Id,
            CreatedAt = employee.CreatedAt,
            FullName = employee.FullName,
            PhotoUrl = employee.PhotoUrl,
            Position = employee.Position,
            Department = employee.Department,
            DateOfBirth = employee.DateOfBirth,
            HireDate = employee.HireDate,
            UserId = employee.UserId,
            ManagerId = employee.ManagerId,
            User = null,
            Manager = null,
            Subordinates = null,
            Contracts = null,
            Salaries = null,
            Vacations = null,
            Equipments = null,
            PassportInfo = null,
            Email = null,
            PhoneNumber = null,
            Telegram = null,
            IsEmailPublic = false,
            IsTelegramPublic = false
        };

        switch (role)
        {
            case "Employee":
                // Faqat ochiq ma'lumotlar va o'zining otpusklari
                filteredEmployee.Email = employee.IsEmailPublic ? employee.Email : null;
                filteredEmployee.PhoneNumber = employee.IsEmailPublic ? employee.PhoneNumber : null;
                filteredEmployee.Telegram = employee.IsTelegramPublic ? employee.Telegram : null;
                filteredEmployee.IsEmailPublic = employee.IsEmailPublic;
                filteredEmployee.IsTelegramPublic = employee.IsTelegramPublic;
                filteredEmployee.Vacations = employee.Vacations?.Select(v => new VacationRequest
                {
                    Id = v.Id,
                    CreatedAt = v.CreatedAt,
                    VacationType = v.VacationType,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    Status = v.Status,
                    EmployeeId = v.EmployeeId
                }).ToList();
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => new Employee
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    PhotoUrl = s.PhotoUrl,
                    Position = s.Position,
                    Department = s.Department,
                    DateOfBirth = s.DateOfBirth,
                    Email = s.IsEmailPublic ? s.Email : null,
                    PhoneNumber = s.IsEmailPublic ? s.PhoneNumber : null,
                    Telegram = s.IsTelegramPublic ? s.Telegram : null,
                    IsEmailPublic = s.IsEmailPublic,
                    IsTelegramPublic = s.IsTelegramPublic,
                    Vacations = s.Vacations?.OrderByDescending(v => v.EndDate).Take(1).ToList()
                }).ToList();
                break;

            case "HR":
                // To'liq ma'lumotlar, kontraktlar, otpusklar, texnika
                filteredEmployee.Email = employee.Email;
                filteredEmployee.PhoneNumber = employee.PhoneNumber;
                filteredEmployee.Telegram = employee.Telegram;
                filteredEmployee.PassportInfo = employee.PassportInfo;
                filteredEmployee.Manager = employee.Manager == null ? null : new Employee
                {
                    Id = employee.Manager.Id,
                    FullName = employee.Manager.FullName,
                    Position = employee.Manager.Position
                };
                filteredEmployee.Contracts = employee.Contracts?.Select(c => new Contract
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
                filteredEmployee.Salaries = employee.Salaries?.Select(s => new Salary
                {
                    Id = s.Id,
                    BaseSalary = s.BaseSalary,
                    Bonus = s.Bonus,
                    Deduction = s.Deduction,
                    StartPeriod = s.StartPeriod,
                    EndPeriod = s.EndPeriod,
                    EmployeeId = s.EmployeeId
                }).ToList();
                filteredEmployee.Vacations = employee.Vacations?.Select(v => new VacationRequest
                {
                    Id = v.Id,
                    VacationType = v.VacationType,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    Status = v.Status,
                    EmployeeId = v.EmployeeId
                }).ToList();
                filteredEmployee.Equipments = employee.Equipments?.Select(e => new EquipmentAssignment
                {
                    Id = e.Id,
                    Type = e.Type,
                    Model = e.Model,
                    InventoryNumber = e.InventoryNumber,
                    AssignmentDate = e.AssignmentDate,
                    Status = e.Status,
                    EmployeeId = e.EmployeeId
                }).ToList();
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => new Employee
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Position = s.Position
                }).ToList();
                break;

            case "Accountant":
                // Faqat maosh ma'lumotlari va ism, lavozim
                filteredEmployee.Salaries = employee.Salaries?.Select(s => new Salary
                {
                    Id = s.Id,
                    BaseSalary = s.BaseSalary,
                    Bonus = s.Bonus,
                    Deduction = s.Deduction,
                    StartPeriod = s.StartPeriod,
                    EndPeriod = s.EndPeriod,
                    EmployeeId = s.EmployeeId
                }).ToList();
                break;

            case "Manager":
                // Faqat o'zining va podchinennilarining ma'lumotlari
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => new Employee
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Position = s.Position,
                    Vacations = s.Vacations?.Select(v => new VacationRequest
                    {
                        Id = v.Id,
                        VacationType = v.VacationType,
                        StartDate = v.StartDate,
                        EndDate = v.EndDate,
                        Status = v.Status,
                        EmployeeId = v.EmployeeId
                    }).ToList(),
                    Email = s.IsEmailPublic ? s.Email : null,
                    PhoneNumber = s.IsEmailPublic ? s.PhoneNumber : null,
                    Telegram = s.IsTelegramPublic ? s.Telegram : null,
                    IsEmailPublic = s.IsEmailPublic,
                    IsTelegramPublic = s.IsTelegramPublic
                }).ToList();
                break;

            case "Admin":
                // To'liq ma'lumotlar, audit uchun hammasi
                filteredEmployee.Email = employee.Email;
                filteredEmployee.PhoneNumber = employee.PhoneNumber;
                filteredEmployee.Telegram = employee.Telegram;
                filteredEmployee.PassportInfo = employee.PassportInfo;
                filteredEmployee.Manager = employee.Manager == null ? null : new Employee
                {
                    Id = employee.Manager.Id,
                    FullName = employee.Manager.FullName,
                    Position = employee.Manager.Position
                };
                filteredEmployee.Contracts = employee.Contracts?.Select(c => new Contract
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
                filteredEmployee.Salaries = employee.Salaries?.Select(s => new Salary
                {
                    Id = s.Id,
                    BaseSalary = s.BaseSalary,
                    Bonus = s.Bonus,
                    Deduction = s.Deduction,
                    StartPeriod = s.StartPeriod,
                    EndPeriod = s.EndPeriod,
                    EmployeeId = s.EmployeeId
                }).ToList();
                filteredEmployee.Vacations = employee.Vacations?.Select(v => new VacationRequest
                {
                    Id = v.Id,
                    VacationType = v.VacationType,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    Status = v.Status,
                    EmployeeId = v.EmployeeId
                }).ToList();
                filteredEmployee.Equipments = employee.Equipments?.Select(e => new EquipmentAssignment
                {
                    Id = e.Id,
                    Type = e.Type,
                    Model = e.Model,
                    InventoryNumber = e.InventoryNumber,
                    AssignmentDate = e.AssignmentDate,
                    Status = e.Status,
                    EmployeeId = e.EmployeeId
                }).ToList();
                filteredEmployee.Subordinates = employee.Subordinates?.Select(s => new Employee
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Position = s.Position
                }).ToList();
                break;

            default:
                throw new ApiException("Invalid role specified.");
        }

        result.EmployeeProfile = filteredEmployee;
        return result;
    }
}

    //public async Task<User?> GetUserInfoByRoleAsync(Guid userId, string role)
    //{
    //    var userQuery = _context.Users
    //        .Where(u => u.Id == userId)
    //        .Include(u => u.EmployeeProfile)
    //            .ThenInclude(e => e.Manager)
    //        .Include(u => u.EmployeeProfile)
    //            .ThenInclude(e => e.Subordinates)
    //        .Include(u => u.EmployeeProfile)
    //            .ThenInclude(e => e.Contracts)
    //        .Include(u => u.EmployeeProfile)
    //            .ThenInclude(e => e.Salaries)
    //        .Include(u => u.EmployeeProfile)
    //            .ThenInclude(e => e.Vacations)
    //        .Include(u => u.EmployeeProfile)
    //            .ThenInclude(e => e.Equipments);

//    var user = await userQuery.FirstOrDefaultAsync();
//    if (user == null)
//        return null;

//    var employee = user.EmployeeProfile;

//    switch (role)
//    {
//        case "Employee":
//            // Faqat ochiq ma'lumotlar va o'zining otpusklari
//            return new
//            {
//                user.Username,
//                user.Email,
//                Role = user.Role.ToString(),
//                Employee = employee == null ? null : new
//                {
//                    employee.FullName,
//                    employee.PhotoUrl,
//                    employee.Position,
//                    employee.Department,
//                    Age = (int)((DateTime.UtcNow - employee.DateOfBirth).TotalDays / 365.25),
//                    Contacts = new
//                    {
//                        Email = employee.IsEmailPublic ? employee.Email : null,
//                        PhoneNumber = employee.IsEmailPublic ? employee.PhoneNumber : null,
//                        Telegram = employee.IsTelegramPublic ? employee.Telegram : null
//                    },
//                    // Faqat o'zining otpusklari
//                    Vacations = employee.Vacations?.Select(v => new
//                    {
//                        v.VacationType,
//                        v.StartDate,
//                        v.EndDate,
//                        v.Status
//                    }).ToList(),
//                    // Kollegalar (subordinates) - faqat ochiq ma'lumotlar
//                    Colleagues = employee.Subordinates?.Select(s => new
//                    {
//                        s.FullName,
//                        s.PhotoUrl,
//                        s.Position,
//                        s.Department,
//                        Age = (int)((DateTime.UtcNow - s.DateOfBirth).TotalDays / 365.25),
//                        Contacts = new
//                        {
//                            Email = s.IsEmailPublic ? s.Email : null,
//                            PhoneNumber = s.IsEmailPublic ? s.PhoneNumber : null,
//                            Telegram = s.IsTelegramPublic ? s.Telegram : null
//                        },
//                        // Kolleganing oxirgi otpuski tugash sanasi
//                        LastVacationEnd = s.Vacations?.OrderByDescending(v => v.EndDate).FirstOrDefault()?.EndDate
//                    }).ToList()
//                }
//            };

//        case "HR":
//            // To'liq ma'lumotlar, kontraktlar, otpusklar, texnika
//            return new
//            {
//                user.Username,
//                user.Email,
//                Role = user.Role.ToString(),
//                Employee = employee == null ? null : new
//                {
//                    employee.FullName,
//                    employee.PhotoUrl,
//                    employee.DateOfBirth,
//                    employee.Email,
//                    employee.PhoneNumber,
//                    employee.Telegram,
//                    employee.Position,
//                    employee.Department,
//                    employee.HireDate,
//                    employee.PassportInfo,
//                    Manager = employee.Manager == null ? null : new
//                    {
//                        employee.Manager.FullName,
//                        employee.Manager.Position
//                    },
//                    Contracts = employee.Contracts?.Select(c => new
//                    {
//                        c.ContractType,
//                        c.StartDate,
//                        c.EndDate,
//                        c.Terms,
//                        c.DocumentUrl,
//                        c.DocumentName,
//                        c.DocumentType
//                    }).ToList(),
//                    Salaries = employee.Salaries?.Select(s => new
//                    {
//                        s.BaseSalary,
//                        s.Bonus,
//                        s.Deduction,
//                        s.StartPeriod,
//                        s.EndPeriod
//                    }).ToList(),
//                    Vacations = employee.Vacations?.Select(v => new
//                    {
//                        v.VacationType,
//                        v.StartDate,
//                        v.EndDate,
//                        v.Status
//                    }).ToList(),
//                    Equipments = employee.Equipments?.Select(e => new
//                    {
//                        e.Type,
//                        e.Model,
//                        e.InventoryNumber,
//                        e.AssignmentDate,
//                        e.Status
//                    }).ToList(),
//                    Subordinates = employee.Subordinates?.Select(s => new
//                    {
//                        s.FullName,
//                        s.Position
//                    }).ToList()
//                }
//            };

//        case "Accountant":
//            // Faqat maosh ma'lumotlari va ism, lavozim
//            return new
//            {
//                user.Username,
//                Role = user.Role.ToString(),
//                Employee = employee == null ? null : new
//                {
//                    employee.FullName,
//                    employee.Position,
//                    Salaries = employee.Salaries?.Select(s => new
//                    {
//                        s.BaseSalary,
//                        s.Bonus,
//                        s.Deduction,
//                        s.StartPeriod,
//                        s.EndPeriod
//                    }).ToList()
//                }
//            };

//        case "Manager":
//            // Faqat o'zining va podchinennilarining ma'lumotlari
//            return new
//            {
//                user.Username,
//                Role = user.Role.ToString(),
//                Employee = employee == null ? null : new
//                {
//                    employee.FullName,
//                    employee.Position,
//                    Subordinates = employee.Subordinates?.Select(s => new
//                    {
//                        s.FullName,
//                        s.Position,
//                        Vacations = s.Vacations?.Select(v => new
//                        {
//                            v.VacationType,
//                            v.StartDate,
//                            v.EndDate,
//                            v.Status
//                        }).ToList(),
//                        Contacts = new
//                        {
//                            Email = s.IsEmailPublic ? s.Email : null,
//                            PhoneNumber = s.IsEmailPublic ? s.PhoneNumber : null,
//                            Telegram = s.IsTelegramPublic ? s.Telegram : null
//                        }
//                    }).ToList()
//                }
//            };

//        case "Admin":
//            // To'liq ma'lumotlar, audit uchun hammasi
//            return new
//            {
//                user.Username,
//                user.Email,
//                user.Role,
//                Employee = employee == null ? null : new
//                {
//                    employee.FullName,
//                    employee.PhotoUrl,
//                    employee.DateOfBirth,
//                    employee.Email,
//                    employee.PhoneNumber,
//                    employee.Telegram,
//                    employee.Position,
//                    employee.Department,
//                    employee.HireDate,
//                    employee.PassportInfo,
//                    Manager = employee.Manager == null ? null : new
//                    {
//                        employee.Manager.FullName,
//                        employee.Manager.Position
//                    },
//                    Contracts = employee.Contracts?.Select(c => new
//                    {
//                        c.ContractType,
//                        c.StartDate,
//                        c.EndDate,
//                        c.Terms,
//                        c.DocumentUrl,
//                        c.DocumentName,
//                        c.DocumentType
//                    }).ToList(),
//                    Salaries = employee.Salaries?.Select(s => new
//                    {
//                        s.BaseSalary,
//                        s.Bonus,
//                        s.Deduction,
//                        s.StartPeriod,
//                        s.EndPeriod
//                    }).ToList(),
//                    Vacations = employee.Vacations?.Select(v => new
//                    {
//                        v.VacationType,
//                        v.StartDate,
//                        v.EndDate,
//                        v.Status
//                    }).ToList(),
//                    Equipments = employee.Equipments?.Select(e => new
//                    {
//                        e.Type,
//                        e.Model,
//                        e.InventoryNumber,
//                        e.AssignmentDate,
//                        e.Status
//                    }).ToList(),
//                    Subordinates = employee.Subordinates?.Select(s => new
//                    {
//                        s.FullName,
//                        s.Position
//                    }).ToList()
//                }
//            };

//        default:
//            throw new ApiException("Invalid role specified.");
//    }
//}
//-------------------------------------------------------------------------------------------
//public IQueryable<User> QueryUserById(Guid id, bool track)
//{
//    var query = _context.Users.Where(x => x.Id == id);
//    return track ? query.AsTracking() : query.AsNoTracking();
//}
//public async Task<User?> GetUserInfoByRoleAsync(Guid id, string role)
//{
//    var user = QueryUserById(id, true);

//    return role switch
//    {
//        "Employee" => await user
//                            .Include(u => u.EmployeeProfile!)
//                                .ThenInclude(e => e.FullName)
//                            .Include(u => u.EmployeeProfile!.Subordinates!)
//                            .FirstOrDefaultAsync(),
//        "HR" => await user
//                            .Include(u => u.EmployeeProfile!)
//                            .ThenInclude(e => e.Manager)
//                            .Include(u => u.EmployeeProfile!.Subordinates!)
//                            .FirstOrDefaultAsync(),
//        "Accountant" => await user
//                            .Include(u => u.EmployeeProfile!)
//                                .ThenInclude(e => e.Contracts!)
//                            .Include(u => u.EmployeeProfile!.Department)
//                            .FirstOrDefaultAsync(),
//        "Manager" => await user
//                            .Include(u => u.EmployeeProfile!)
//                                .ThenInclude(e => e.Manager)
//                            .FirstOrDefaultAsync(),
//        "Admin" => await user
//                            .Include(u => u.EmployeeProfile!)
//                                .ThenInclude(e => e.Manager)
//                            .Include(u => u.EmployeeProfile!.Subordinates!)
//                            .FirstOrDefaultAsync(),
//        _ => throw new ApiException("Invalid role specified."),
//    };
//}
//}


using HR_System.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<VacationRequest> VacationRequests { get; set; }
    public DbSet<EquipmentAssignment> EquipmentAssignments { get; set; }
    public DbSet<User> Users { get; set; }
}

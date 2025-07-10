using HR_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<VacationRequest> VacationRequests { get; set; }
    public DbSet<EquipmentAssignment> EquipmentAssignments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
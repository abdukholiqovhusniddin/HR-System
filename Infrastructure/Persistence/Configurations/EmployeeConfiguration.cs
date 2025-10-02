using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.FullName).IsRequired().HasMaxLength(150);
        builder.Property(e => e.Email).HasMaxLength(100);
        builder.Property(e => e.Telegram).HasMaxLength(100);
        builder.Property(e => e.PhoneNumber).HasMaxLength(30);
        builder.Property(e => e.Position).HasMaxLength(100);
        builder.Property(e => e.Department).HasMaxLength(100);
        builder.Property(e => e.PassportInfo).HasMaxLength(200);

        builder.HasOne(e => e.Manager)
               .WithMany(e => e.Subordinates)
               .HasForeignKey(e => e.ManagerId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.Property(u=>u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(c => c.Contracts)
               .WithOne(c => c.Employee)
               .HasForeignKey(c => c.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Salaries)
               .WithOne(s => s.Employee)
               .HasForeignKey(s => s.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Employee
            {
                Id = new Guid("d0ef2057-73ee-4092-80ea-b891a9947eae"),
                FullName = "Admin User",
                Email = "abdukholiqovh@gmail.com",
                PhoneNumber = "+998901234567",
                Position = "Administrator",
                Department = "Administration",
                DateOfBirth = new DateTime(2005, 02, 11),
                IsEmailPublic = true,
                IsTelegramPublic = true,
                Telegram = "admin_telegram",
                PassportInfo = "AB1234567",
                CreatedAt = DateTime.UtcNow,
                UserId = new Guid("1cd5ed74-9327-4446-9c76-adfc28d3f5bd")
            }
        );
    }
}

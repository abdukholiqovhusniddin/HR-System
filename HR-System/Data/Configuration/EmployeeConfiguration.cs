using HR_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_System.Data.Configuration;
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
        builder.Property(e => e.PhotoUrl).HasMaxLength(500);
        builder.Property(e => e.PassportInfo).HasMaxLength(200);

        builder.HasOne(e => e.Manager)
               .WithMany(e => e.Subordinates)
               .HasForeignKey(e => e.ManagerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Employee
            {
                Id = new Guid("7b9b6dcb-79ea-4f1f-9c6e-02f1cf23f19a"),
                FullName = "Admin User",
                Email = "abdukholiqovh@gmail.com",
                PhoneNumber = "+998901234567",
                Position = "Administrator",
                Department = "Administration",
                DateOfBirth = new DateTime(1990, 1, 1),
                IsEmailPublic = true,
                IsTelegramPublic = true,
                Telegram = "admin_telegram",
                PhotoUrl = "https://example.com/admin_photo.jpg",
                PassportInfo = "AB1234567",
                CreatedAt = DateTime.UtcNow,
                ManagerId = new Guid("7b9b6dcb-79ea-4f1f-9c6e-02f1cf23f19a"),
                UserId = new Guid("7b9b6dcb-79ea-4f1f-9c6e-02f1cf23f18e")
            }
        );
    }
}

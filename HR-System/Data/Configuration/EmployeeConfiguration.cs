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
        builder.Property(e => e.PhotoUrl).HasMaxLength(250);
        builder.Property(e => e.PassportInfo).HasMaxLength(200);

        builder.HasOne(e => e.Manager)
               .WithMany(e => e.Subordinates)
               .HasForeignKey(e => e.ManagerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

using HR_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_System.Data.Configuration;
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.PasswordHash)
            .IsRequired().
            HasMaxLength(256);

        builder.HasOne(u => u.EmployeeProfile)
               .WithOne(e => e.User)
               .HasForeignKey<Employee>(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();

        builder.HasData(
            new User
            {
                Id = new Guid("7b9b6dcb-79ea-4f1f-9c6e-02f1cf23f18e"),
                Username = "admin",
                Email = "test@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = UserRole.Admin,
            });
    }
}
using HR_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_System.Data.Configuration;
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);

        builder.HasIndex(u => u.Username).IsUnique();
    }
}
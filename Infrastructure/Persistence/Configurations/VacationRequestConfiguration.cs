using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class VacationRequestConfiguration : IEntityTypeConfiguration<Vacation>
{
    public void Configure(EntityTypeBuilder<Vacation> builder)
    {
        builder.Property(v => v.VacationType).HasMaxLength(100);
        builder.Property(v => v.Status).HasMaxLength(50);
    }
}
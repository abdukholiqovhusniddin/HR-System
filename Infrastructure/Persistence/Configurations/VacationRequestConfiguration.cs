using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class VacationRequestConfiguration : IEntityTypeConfiguration<Vacations>
{
    public void Configure(EntityTypeBuilder<Vacations> builder)
    {
        builder.Property(v => v.VacationType).HasMaxLength(100);
        builder.Property(v => v.Status).HasMaxLength(50);
    }
}
using HR_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_System.Data.Configuration;
public class VacationRequestConfiguration : IEntityTypeConfiguration<VacationRequest>
{
    public void Configure(EntityTypeBuilder<VacationRequest> builder)
    {
        builder.Property(v => v.VacationType).HasMaxLength(100);
        builder.Property(v => v.Status).HasMaxLength(50);
    }
}
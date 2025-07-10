using HR_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_System.Data.Configuration;
public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.Property(c => c.ContractType).HasMaxLength(50);
        builder.Property(c => c.Terms).HasMaxLength(1000);
        builder.Property(c => c.DocumentUrl).HasMaxLength(250);
        builder.Property(c => c.DocumentName).HasMaxLength(150);
        builder.Property(c => c.DocumentType).HasMaxLength(50);
    }
}

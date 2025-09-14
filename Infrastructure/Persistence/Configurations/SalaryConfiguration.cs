using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class SalaryConfiguration: IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.Property(s => s.BaseSalary).HasPrecision(18, 2);
        builder.Property(s => s.Bonus).HasPrecision(18, 2);
        builder.Property(s => s.Deduction).HasPrecision(18, 2);
    }
}
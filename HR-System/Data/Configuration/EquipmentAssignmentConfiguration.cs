using HR_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_System.Data.Configuration;
public class EquipmentAssignmentConfiguration: IEntityTypeConfiguration<EquipmentAssignment>
{
    public void Configure(EntityTypeBuilder<EquipmentAssignment> builder)
    {
        builder.Property(e => e.Type).HasMaxLength(100);
        builder.Property(e => e.Model).HasMaxLength(150);
        builder.Property(e => e.InventoryNumber).HasMaxLength(100);
        builder.Property(e => e.Status).HasMaxLength(50);
    }
}

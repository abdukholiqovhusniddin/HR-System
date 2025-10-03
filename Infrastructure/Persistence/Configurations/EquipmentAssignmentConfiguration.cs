using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class EquipmentAssignmentConfiguration: IEntityTypeConfiguration<Equipments>
{
    public void Configure(EntityTypeBuilder<Equipments> builder)
    {
        builder.Property(e => e.Type).HasMaxLength(100);
        builder.Property(e => e.Model).HasMaxLength(150);
        builder.Property(e => e.InventoryNumber).HasMaxLength(100);
        builder.Property(e => e.Status).HasMaxLength(50);
    }
}

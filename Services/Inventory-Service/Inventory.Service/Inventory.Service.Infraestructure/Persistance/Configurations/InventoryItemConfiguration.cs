using Inventory.Service.Domain.AgregatesRoots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Service.Infraestructure.Persistance.Configurations;

public class InventoryItemConfiguration :  IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("getdate()");
        
        builder.Property(i => i.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("getdate()");

        builder.HasOne(i => i.Inventory)
            .WithMany(i => i.Items)
            .HasForeignKey(i => i.InventoryId);
    }
}
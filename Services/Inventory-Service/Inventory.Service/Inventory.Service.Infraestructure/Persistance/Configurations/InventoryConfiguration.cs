using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Service.Infraestructure.Persistance.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Domain.AgregatesRoots.Inventory>
{
  public void Configure(EntityTypeBuilder<Domain.AgregatesRoots.Inventory> builder)
  {
    builder.HasKey(i => i.Id);

    builder.Property(i => i.CreatedAt)
      .ValueGeneratedOnAdd()
      .HasDefaultValue("GETDATE()");
    
    builder.Property(i => i.CreatedAt)
      .ValueGeneratedOnUpdate()
      .HasDefaultValueSql("GETDATE()");

    builder.HasMany(i => i.Items)
      .WithOne(i => i.Inventory)
      .HasForeignKey(i => i.InventoryId);
  }
  
}
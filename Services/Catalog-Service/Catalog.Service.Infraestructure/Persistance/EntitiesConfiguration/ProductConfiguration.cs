
using Catalog.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Service.Infraestructure.Context.EntitiesConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price)
                .HasPrecision(18,2).HasDefaultValue(0).ValueGeneratedOnAdd().IsRequired();

            builder.Property(p => p.DateCreated).HasDefaultValue("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd().IsRequired();

            builder.Property(p => p.DateUpdated).HasDefaultValue("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate().IsRequired(false);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        }
    }
}

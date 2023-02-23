using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Entities;

namespace Catalog.API.Repository.EntityConfiguration;

public class ProductVariantEntityConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("CatalogProductVariant").HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .UseHiLo("catalog_product_variant_hilo");

        builder.Property(c => c.Label)
            .IsRequired()
            .HasMaxLength(50);  

        builder.Property(c => c.Price)
            .IsRequired();

        builder.Property(c => c.AvailableStock)
            .IsRequired();

        builder.HasOne(c => c.Product)
            .WithMany(c => c.ProductVariants)
            .HasForeignKey(c => c.ProductId);

        builder.HasMany(c => c.Pictures)
            .WithMany(c => c.ProductVariants)
            .UsingEntity(e => e.ToTable("CatalogProductVariantCatalogPicture"));
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Entities;

namespace Catalog.API.Repository.EntityConfiguration;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("CatalogProduct").HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .UseHiLo("catalog_product_hilo");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.PictureFileName)
            .IsRequired()
            .HasMaxLength(128);
        
        builder.HasOne(c => c.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.CategoryId);
        
        builder.HasMany(c => c.Consumers)
            .WithMany(c => c.Products)
            .UsingEntity(e => e.ToTable("CatalogConsumerCatalogProduct"));
    }
}
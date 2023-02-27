using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Entities;

namespace Catalog.API.Repository.EntityConfiguration;

public class ProductVariantPictureEntityConfiguration : IEntityTypeConfiguration<ProductVariantPicture>
{
    public void Configure(EntityTypeBuilder<ProductVariantPicture> builder)
    {
        builder.ToTable("CatalogProductVariantPictures").HasKey(c => new { c.ProductVariantId, c.PictureFileName });

        builder.HasOne(c => c.ProductVariant)
            .WithMany(c => c.ProductVariantPictures)
            .HasForeignKey(c => c.ProductVariantId);

        builder.Property(c => c.PictureFileName)
            .IsRequired()
            .HasMaxLength(128);
    }
}
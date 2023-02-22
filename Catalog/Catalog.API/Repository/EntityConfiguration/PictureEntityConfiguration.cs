using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Entities;

namespace Catalog.API.Repository.EntityConfiguration;

public class PictureEntityConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.ToTable("CatalogPicture").HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .UseHiLo("catalog_picture_hilo");

        builder.Property(c => c.PictureFileName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
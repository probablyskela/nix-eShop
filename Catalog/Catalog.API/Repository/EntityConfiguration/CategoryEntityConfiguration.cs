using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Entities;

namespace Catalog.API.Repository.EntityConfiguration;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("CatalogCategory").HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .UseHiLo("catalog_category_hilo");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
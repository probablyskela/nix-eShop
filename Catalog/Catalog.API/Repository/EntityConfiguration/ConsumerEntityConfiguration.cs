using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Entities;

namespace Catalog.API.Repository.EntityConfiguration;

public class ConsumerEntityConfiguration : IEntityTypeConfiguration<Consumer>
{
    public void Configure(EntityTypeBuilder<Consumer> builder)
    {
        builder.ToTable("CatalogConsumer").HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .UseHiLo("catalog_consumer_hilo");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
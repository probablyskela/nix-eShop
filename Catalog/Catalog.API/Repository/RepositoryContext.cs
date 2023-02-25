using Catalog.API.Repository.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;

namespace Catalog.API.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product>? Items;
    public DbSet<Category>? Categories;
    public DbSet<Consumer>? Consumers;
    public DbSet<ProductVariant>? ProductVariants;
    public DbSet<ProductVariantPicture>? ProductVariantPictures;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ConsumerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantPictureEntityConfiguration());
    }
}
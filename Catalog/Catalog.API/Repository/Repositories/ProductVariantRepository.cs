using Catalog.API.Repository.Extensions;
using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class ProductVariantRepository : RepositoryBase<ProductVariant>, IProductVariantRepository
{
    public ProductVariantRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<ProductVariant>> GetProductVariantsAsync(int productId,
        ProductVariantParameters productVariantParameters, bool trackChanges)
    {
        var productVariants = FindByCondition(p => p.ProductId.Equals(productId), trackChanges)
            .Include(p => p.Product)
            .Include(p => p.ProductVariantPictures)
            .FilterPrice(productVariantParameters.MinPrice, productVariantParameters.MaxPrice,
                productVariantParameters.ValidPriceRange)
            .Sort(productVariantParameters.OrderBy);

        return await PagedList<ProductVariant>
            .ToPagedList(productVariants, productVariantParameters.PageIndex, productVariantParameters.PageSize);
    }

    public async Task<ProductVariant?> GetProductVariantAsync(int productId, int productVariantId, bool trackChanges)
    {
        return await FindByCondition(p => p.ProductId.Equals(productId) && p.Id.Equals(productVariantId), trackChanges)
            .Include(p => p.Product)
            .Include(p => p.ProductVariantPictures)
            .SingleOrDefaultAsync();
    }

    public async Task CreateProductVariantAsync(ProductVariant productVariant)
    {
        await CreateAsync(productVariant);
    }

    public void DeleteProductVariant(ProductVariant productVariant)
    {
        Delete(productVariant);
    }
}
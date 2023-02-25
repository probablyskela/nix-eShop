using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class ProductVariantPictureRepository : RepositoryBase<ProductVariantPicture>, IProductVariantPictureRepository
{
    public ProductVariantPictureRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<ProductVariantPicture?> GetProductVariantPictureAsync(int productVariantId,
        string pictureFIleName, bool trackChanges)
    {
        return await FindByCondition(
                p => p.ProductVariantId == productVariantId && p.PictureFileName == pictureFIleName, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task CreateProductVariantPictureAsync(ProductVariantPicture productVariantPicture)
    {
        await CreateAsync(productVariantPicture);
    }

    public void DeleteProductVariantPicture(ProductVariantPicture productVariantPicture)
    {
        Delete(productVariantPicture);
    }
}
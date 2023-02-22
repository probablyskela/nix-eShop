using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Repository.Repositories.Abstractions;

public interface IProductVariantRepository
{
    Task<PagedList<ProductVariant>> GetProductVariantsAsync(int productId,
        ProductVariantParameters productVariantParameters, bool trackChanges);

    Task<ProductVariant?> GetProductVariantAsync(int productId, int productVariantId, bool trackChanges);
}
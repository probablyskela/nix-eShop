using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services.Abstractions;

public interface IProductVariantService
{
    Task<(IEnumerable<ProductVariantDto> productVariantDtos, MetaData metaData)> GetProductVariantsAsync(
        int productId, ProductVariantParameters productVariantParameters, bool trackChanges);

    Task<ProductVariantDto> GetProductVariantAsync(int productId, int productVariantId, bool trackChanges);
    Task<ProductVariantDto> CreateProductVariantAsync(int productId, ProductVariantForCreationDto productVariantForCreation);
}
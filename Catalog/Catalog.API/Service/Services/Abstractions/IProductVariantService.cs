using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services.Abstractions;

public interface IProductVariantService
{
    Task<(IEnumerable<ProductVariantDto> productVariantDtos, MetaData metaData)> GetProductVariantsAsync(
        int productId, ProductVariantParameters productVariantParameters, bool trackChanges);

    Task<ProductVariantDto> GetProductVariantAsync(int productId, int productVariantId, bool trackChanges);

    Task<ProductVariantDto> CreateProductVariantAsync(int productId,
        ProductVariantForCreationDto productVariantForCreation);

    Task UpdateProductVariantLabelAsync(int productId, int productVariantId,
        ProductVariantUpdateLabelDto productVariantUpdateLabelDto);

    Task UpdateProductVariantPriceAsync(int productId, int productVariantId,
        ProductVariantUpdatePriceDto productVariantUpdatePriceDto);

    Task UpdateProductVariantAvailableStockAsync(int productId, int productVariantId,
        ProductVariantUpdateAvailableStockDto productVariantUpdateAvailableStockDto);

    Task UpdateProductVariantAddPictureAsync(int productId, int productVariantId,
        ProductVariantUpdatePictureFileNameDto productVariantUpdatePictureFileNameDto);

    Task UpdateProductVariantRemovePictureAsync(int productId, int productVariantId,
        ProductVariantUpdatePictureFileNameDto productVariantUpdatePictureFileNameDto);

    Task DeleteProductVariantAsync(int productId, int productVariantId, bool trackChanges);
}
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services.Abstractions;

public interface IProductService
{
    Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetProductsAsync(ProductParameters productParameters,
        bool trackChanges);

    Task<ProductDto> GetProductAsync(int productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationDto productForCreation);
    Task UpdateProductNameAsync(int productId, ProductUpdateNameDto productUpdateNameDto);
    Task UpdateProductDescriptionAsync(int productId, ProductUpdateDescriptionDto productUpdateDescriptionDto);
    Task UpdateProductPictureFileNameAsync(int productId, ProductUpdatePictureFileNameDto productUpdatePictureFileNameDto);
    Task UpdateProductCategoryAsync(int productId, ProductUpdateCategoryDto productUpdateCategoryDto);
    Task UpdateProductAddConsumerAsync(int productId, ProductUpdateConsumerDto productUpdateConsumerDto);
    Task UpdateProductRemoveConsumerAsync(int productId, ProductUpdateConsumerDto productUpdateConsumerDto);
    Task DeleteProductAsync(int productId, bool trackChanges);
}
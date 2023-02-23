using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Requests.RequestFeatures;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services.Abstractions;

public interface IProductService
{
    Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetProductsAsync(ProductParameters productParameters,
        bool trackChanges);

    Task<ProductDto> GetProductAsync(int productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationDto productForCreation);
}
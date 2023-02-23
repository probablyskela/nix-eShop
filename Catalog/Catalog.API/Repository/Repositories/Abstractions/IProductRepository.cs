using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Repository.Repositories.Abstractions;

public interface IProductRepository
{
    Task<PagedList<Product>> GetProductsAsync(ProductParameters productParameters, bool trackChanges);
    Task<Product?> GetProductAsync(int productId, bool trackChanges);
    Task CreateProductAsync(Product product);
}
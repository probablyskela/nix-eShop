using Catalog.API.Repository.Extensions;
using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Product>> GetProductsAsync(ProductParameters productParameters, bool trackChanges)
    {
        var products = FindAll(trackChanges)
            .Search(productParameters.SearchTerm)
            .FilterConsumers(productParameters.Consumers)
            .Sort(productParameters.OrderBy);

        return await PagedList<Product>
            .ToPagedList(products, productParameters.PageIndex, productParameters.PageSize);
    }

    public async Task<Product?> GetProductAsync(int productId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(productId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
using Catalog.API.Repository.Extensions;
using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class ConsumerRepository : RepositoryBase<Consumer>, IConsumerRepository
{
    public ConsumerRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Consumer>> GetConsumersAsync(ConsumerParameters consumerParameters, bool trackChanges)
    {
        var categories = FindAll(trackChanges)
            .Sort(consumerParameters.OrderBy)
            .Include(c => c.Products);

        return await PagedList<Consumer>
            .ToPagedList(categories, consumerParameters.PageIndex, consumerParameters.PageSize);
    }

    public async Task<Consumer?> GetConsumerAsync(int consumerId, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(consumerId), trackChanges)
            .Include(c => c.Products)
            .SingleOrDefaultAsync();
    }

    public async Task CreateConsumerAsync(Consumer consumer)
    {
        await CreateAsync(consumer);
    }
}
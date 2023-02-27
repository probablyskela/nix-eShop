using System.Collections;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Repository.Repositories.Abstractions;

public interface IConsumerRepository
{
    Task<PagedList<Consumer>> GetConsumersAsync(ConsumerParameters consumerParameters, bool trackChanges);
    Task<IEnumerable<Consumer>> GetConsumersByIdsAsync(IEnumerable<int> consumerIds, bool trackChanges);
    Task<Consumer?> GetConsumerAsync(int consumerId, bool trackChanges);
    Task CreateConsumerAsync(Consumer consumer);
    void DeleteConsumer(Consumer consumer);
}
using Shared.Data.Dtos.ConsumerDtos;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services.Abstractions;

public interface IConsumerService
{
    Task<(IEnumerable<ConsumerDto> consumerDtos, MetaData metaData)> GetConsumersAsync(
        ConsumerParameters consumerParameters, bool trackChanges);

    Task<ConsumerDto> GetConsumerAsync(int consumerId, bool trackChanges);
    Task<ConsumerDto> CreateConsumerAsync(ConsumerForCreationDto consumerForCreation);
    Task UpdateConsumerNameAsync(int consumerId, ConsumerUpdateNameDto consumerUpdateNameDto);
    Task DeleteConsumerAsync(int consumerId, bool trackChanges);
}
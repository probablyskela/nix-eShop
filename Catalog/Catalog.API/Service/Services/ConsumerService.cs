using AutoMapper;
using Catalog.API.Exceptions.NotFoundExceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Shared.Data.Dtos.ConsumerDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services;

public class ConsumerService : IConsumerService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<ConsumerService> _logger;
    private readonly IMapper _mapper;

    public ConsumerService(IRepositoryManager repositoryManager, ILogger<ConsumerService> logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<ConsumerDto> consumerDtos, MetaData metaData)> GetConsumersAsync(
        ConsumerParameters consumerParameters, bool trackChanges)
    {
        var consumerEntities = await _repository.Consumer.GetConsumersAsync(consumerParameters, trackChanges);

        var consumerDtos = _mapper.Map<IEnumerable<ConsumerDto>>(consumerEntities);

        _logger.LogInformation(
            $"Returned consumers on page: {consumerParameters.PageIndex} with {consumerParameters.PageSize} elements");

        return (consumerDtos, consumerEntities.MetaData);
    }

    public async Task<ConsumerDto> GetConsumerAsync(int consumerId, bool trackChanges)
    {
        var consumerEntity = await GetConsumerIfExistsAsync(consumerId, trackChanges);

        var consumerDto = _mapper.Map<ConsumerDto>(consumerEntity);

        _logger.LogInformation($"Returned consumer with id: {consumerId}");

        return consumerDto;
    }

    public async Task<ConsumerDto> CreateConsumerAsync(ConsumerForCreationDto consumerForCreation)
    {
        var consumerEntity = _mapper.Map<Consumer>(consumerForCreation);

        await _repository.Consumer.CreateConsumerAsync(consumerEntity);
        await _repository.SaveAsync();

        var consumerDto = _mapper.Map<ConsumerDto>(consumerEntity);

        _logger.LogInformation($"Created consumer with id: {consumerDto.Id}");

        return consumerDto;
    }

    public async Task UpdateConsumerNameAsync(int consumerId, ConsumerUpdateNameDto consumerUpdateNameDto)
    {
        var consumer = await GetConsumerIfExistsAsync(consumerId, trackChanges: true);

        consumer.Name = consumerUpdateNameDto.Name;

        _logger.LogInformation($"Updated consumer name with id: {consumerId}");

        await _repository.SaveAsync();
    }

    public async Task DeleteConsumerAsync(int consumerId, bool trackChanges)
    {
        var consumer = await GetConsumerIfExistsAsync(consumerId, trackChanges);

        _repository.Consumer.DeleteConsumer(consumer);
        await _repository.SaveAsync();

        _logger.LogInformation($"Deleted consumer with id: {consumerId}");
    }

    private async Task<Consumer> GetConsumerIfExistsAsync(int consumerId, bool trackChanges)
    {
        var consumerEntity = await _repository.Consumer.GetConsumerAsync(consumerId, trackChanges);

        if (consumerEntity is null)
        {
            throw new ConsumerNotFoundException(consumerId);
        }

        return consumerEntity;
    }
}
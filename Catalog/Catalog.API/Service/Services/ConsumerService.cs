using AutoMapper;
using Catalog.API.Exceptions.NotFoundExceptions;
using Catalog.API.Repository;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
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

        return (consumerDtos, consumerEntities.MetaData);
    }

    public async Task<ConsumerDto> GetConsumerAsync(int consumerId, bool trackChanges)
    {
        var consumerEntity = await GetConsumerIfExistsAsync(consumerId, trackChanges);

        var consumerDto = _mapper.Map<ConsumerDto>(consumerEntity);

        return consumerDto;
    }

    public async Task<ConsumerDto> CreateConsumerAsync(ConsumerForCreationDto consumerForCreation)
    {
        var consumerEntity = _mapper.Map<Consumer>(consumerForCreation);

        await _repository.Consumer.CreateConsumerAsync(consumerEntity);
        await _repository.SaveAsync();

        var consumerDto = _mapper.Map<ConsumerDto>(consumerEntity);

        return consumerDto;
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
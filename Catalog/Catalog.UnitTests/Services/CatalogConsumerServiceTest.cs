using AutoMapper;
using Catalog.API.Exceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Data.Dtos.ConsumerDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.UnitTests.Services;

public class CatalogConsumerServiceTest
{
    private readonly IConsumerService _consumerService;

    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILogger<ConsumerService>> _logger;
    private readonly Mock<IMapper> _mapper;

    public CatalogConsumerServiceTest()
    {
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILogger<ConsumerService>>();
        _mapper = new Mock<IMapper>();

        _consumerService = new ConsumerService(_repository.Object, _logger.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateConsumerAsync_Success()
    {
        // arrange  
        var consumerForCreationDto = new ConsumerForCreationDto
        {
            Name = "test"
        };

        var consumer = new ConsumerDto
        {
            Name = "test"
        };

        _repository.Setup(s => s.Consumer.CreateConsumerAsync(
            It.IsAny<Consumer>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<ConsumerDto>(
            It.IsAny<Consumer>())).Returns(consumer);

        // act
        var result = await _consumerService.CreateConsumerAsync(consumerForCreationDto);

        // assert
        result.Should().NotBeNull();
        result.Name.Should().Be(consumer.Name);
    }

    [Fact]
    public async Task CreateConsumerAsync_Failed()
    {
        // arrange  
        var consumerForCreationDto = new ConsumerForCreationDto
        {
            Name = "test"
        };

        var consumer = new ConsumerDto
        {
            Name = null
        };

        _repository.Setup(s => s.Consumer.CreateConsumerAsync(
            It.IsAny<Consumer>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<ConsumerDto>(
            It.IsAny<Consumer>())).Returns(consumer);

        // act
        var result = await _consumerService.CreateConsumerAsync(consumerForCreationDto);

        // assert
        result.Name.Should().Be(consumer.Name);
    }

    [Fact]
    public async Task GetConsumersAsync_Success()
    {
        // arrange  
        var consumerParameters = new ConsumerParameters();

        var consumerDtos = new List<ConsumerDto>
        {
            new()
            {
                Name = "test"
            }
        };

        var consumerEntities = new PagedList<Consumer>(new List<Consumer>
        {
            new Consumer
            {
                Name = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Consumer.GetConsumersAsync(
            consumerParameters,
            false)).ReturnsAsync(consumerEntities);

        _mapper.Setup(s => s.Map<IEnumerable<ConsumerDto>>(
            It.IsAny<IEnumerable<Consumer>>())).Returns(consumerDtos);

        // act
        var result = await _consumerService.GetConsumersAsync(consumerParameters, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetConsumersAsync_Failed()
    {
        // arrange  
        var consumerParameters = new ConsumerParameters();

        List<ConsumerDto>? consumerDtos = null;

        var consumerEntities = new PagedList<Consumer>(new List<Consumer>
        {
            new Consumer
            {
                Name = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Consumer.GetConsumersAsync(
            consumerParameters,
            false)).ReturnsAsync(consumerEntities);

        _mapper.Setup(s => s.Map<IEnumerable<ConsumerDto>>(
            It.IsAny<IEnumerable<Consumer>>())).Returns(consumerDtos);

        // act
        var result = await _consumerService.GetConsumersAsync(consumerParameters, false);

        // assert
        result.consumerDtos.Should().BeNull();
    }

    [Fact]
    public async Task GetConsumerAsync_Success()
    {
        // arrange  
        var consumerDto = new ConsumerDto
        {
            Name = "test"
        };

        var consumerEntity = new Consumer
        {
            Name = "test"
        };

        var consumerId = 1;

        _repository.Setup(s => s.Consumer.GetConsumerAsync(
            It.IsAny<int>(),
            false)).ReturnsAsync(consumerEntity);

        _mapper.Setup(s => s.Map<ConsumerDto>(
            It.IsAny<Consumer>())).Returns(consumerDto);

        // act
        var result = await _consumerService.GetConsumerAsync(consumerId, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetConsumerAsync_Failed()
    {
        // arrange  
        ConsumerDto? consumerDto = null;

        var consumerEntity = new Consumer
        {
            Name = "test"
        };

        var consumerId = 1;

        _repository.Setup(s => s.Consumer.GetConsumerAsync(
            It.IsAny<int>(),
            false)).ReturnsAsync(consumerEntity);

        _mapper.Setup(s => s.Map<ConsumerDto>(
            It.IsAny<Consumer>())).Returns(consumerDto);

        // act
        var result = await _consumerService.GetConsumerAsync(consumerId, false);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateConsumerNameAsync_Success()
    {
        // arrange  
        var consumerId = 1;

        var consumerUpdateNameDto = new ConsumerUpdateNameDto
        {
            Name = "asd"
        };

        // act
        Func<Task> act = async () =>
        {
            await _consumerService.UpdateConsumerNameAsync(consumerId, consumerUpdateNameDto);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateConsumerNameAsync_Failed()
    {
        // arrange  
        var consumerId = 1;

        var consumerUpdateNameDto = new ConsumerUpdateNameDto
        {
            Name = "asd"
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _consumerService.UpdateConsumerNameAsync(consumerId, consumerUpdateNameDto);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task DeleteConsumerAsync_Success()
    {
        // arrange  
        var consumerId = 1;

        _repository.Setup(s => s.Consumer.DeleteConsumer(
            It.IsAny<Consumer>()));

        // act
        Func<Task> act = async () => { await _consumerService.DeleteConsumerAsync(consumerId, false); };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteConsumerAsync_Failed()
    {
        // arrange  
        var consumerId = 1;

        _repository.Setup(s => s.Consumer.DeleteConsumer(
            It.IsAny<Consumer>()));

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () => { await _consumerService.DeleteConsumerAsync(consumerId, false); };
        // assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
using AutoMapper;
using Catalog.API.Exceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.UnitTests.Services;

public class CatalogProductVariantServiceTest
{
    private readonly IProductVariantService _productVariantService;

    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILogger<ProductVariantService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly Product _testProduct = new()
    {
        Name = "test"
    };
    
    private readonly ProductVariant _testProductVariant = new()
    {
        Label = "test",
        
    };
    
    public CatalogProductVariantServiceTest()
    {
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILogger<ProductVariantService>>();
        _mapper = new Mock<IMapper>();

        _productVariantService = new ProductVariantService(_repository.Object, _logger.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateProductVariantAsync_Success()
    {
        // arrange  
        var productId = 1;
        var productVariantForCreationDto = new ProductVariantForCreationDto
        {
            Label = "test",
            PictureFileNames = new List<string>()
        };

        var productVariant = new ProductVariantDto
        {
            Label = "test"
        };

        _repository.Setup(s => s.ProductVariant.CreateProductVariantAsync(
            It.IsAny<ProductVariant>())).Returns(Task.FromResult(default(object)));

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testProduct);
        
        _mapper.Setup(s => s.Map<ProductVariantDto>(
            It.IsAny<ProductVariant>())).Returns(productVariant);
        
        _mapper.Setup(s => s.Map<ProductVariant>(
            It.IsAny<ProductVariantForCreationDto>())).Returns(_testProductVariant);

        // act
        var result = await _productVariantService.CreateProductVariantAsync(productId, productVariantForCreationDto);

        // assert
        result.Should().NotBeNull();
        result.Label.Should().Be(productVariant.Label);
    }

    [Fact]
    public async Task CreateProductVariantAsync_Failed()
    {
        // arrange  
        var productId = 1;
        var productVariantForCreationDto = new ProductVariantForCreationDto
        {
            Label = "test",
            PictureFileNames = new List<string>()
        };

        var productVariant = new ProductVariantDto
        {
            Label = null
        };

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testProduct);

        _mapper.Setup(s => s.Map<ProductVariant>(
            It.IsAny<ProductVariantForCreationDto>())).Returns(_testProductVariant);
        
        _repository.Setup(s => s.ProductVariant.CreateProductVariantAsync(
            It.IsAny<ProductVariant>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<ProductVariantDto>(
            It.IsAny<ProductVariant>())).Returns(productVariant);

        // act
        var result = await _productVariantService.CreateProductVariantAsync(productId, productVariantForCreationDto);

        // assert
        result.Label.Should().Be(productVariant.Label);
    }

    [Fact]
    public async Task GetProductVariantsAsync_Success()
    {
        // arrange  
        int productId = 1;
        var productVariantParameters = new ProductVariantParameters();

        var productVariantDtos = new List<ProductVariantDto>
        {
            new()
            {
                Label = "test"
            }
        };

        var productVariantEntities = new PagedList<ProductVariant>(new List<ProductVariant>
        {
            new ProductVariant
            {
                Label = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.ProductVariant.GetProductVariantsAsync(
            productId,
            productVariantParameters,
            false)).ReturnsAsync(productVariantEntities);

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testProduct);
        
        _mapper.Setup(s => s.Map<IEnumerable<ProductVariantDto>>(
            It.IsAny<IEnumerable<ProductVariant>>())).Returns(productVariantDtos);

        // act
        var result = await _productVariantService.GetProductVariantsAsync(productId, productVariantParameters, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductVariantsAsync_Failed()
    {
        // arrange  
        var productId = 1;
        var productVariantParameters = new ProductVariantParameters();

        List<ProductVariantDto>? productVariantDtos = null;

        var productVariantEntities = new PagedList<ProductVariant>(new List<ProductVariant>
        {
            new ProductVariant
            {
                Label = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testProduct);
        
        _repository.Setup(s => s.ProductVariant.GetProductVariantsAsync(
            productId,
            productVariantParameters,
            false)).ReturnsAsync(productVariantEntities);

        _mapper.Setup(s => s.Map<IEnumerable<ProductVariantDto>>(
            It.IsAny<IEnumerable<ProductVariant>>())).Returns(productVariantDtos);

        // act
        var result = await _productVariantService.GetProductVariantsAsync(productId, productVariantParameters, false);

        // assert
        result.productVariantDtos.Should().BeNull();
    }

    [Fact]
    public async Task GetProductVariantAsync_Success()
    {
        // arrange  
        var productId = 1;
        var productVariantDto = new ProductVariantDto
        {
            Label = "test"
        };

        var productVariantEntity = new ProductVariant
        {
            Label = "test"
        };

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testProduct);
        
        var productVariantId = 1;

        _repository.Setup(s => s.ProductVariant.GetProductVariantAsync(
            It.IsAny<int>(),
            It.IsAny<int>(),
            false)).ReturnsAsync(productVariantEntity);

        _mapper.Setup(s => s.Map<ProductVariantDto>(
            It.IsAny<ProductVariant>())).Returns(productVariantDto);

        // act
        var result = await _productVariantService.GetProductVariantAsync(productId, productVariantId, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductVariantAsync_Failed()
    {
        // arrange  
        var productId = 1;
        ProductVariantDto? productVariantDto = null;

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testProduct);
        
        var productVariantEntity = new ProductVariant
        {
            Label = "test"
        };

        var productVariantId = 1;

        _repository.Setup(s => s.ProductVariant.GetProductVariantAsync(
            It.IsAny<int>(),
            It.IsAny<int>(),
            false)).ReturnsAsync(productVariantEntity);

        _mapper.Setup(s => s.Map<ProductVariantDto>(
            It.IsAny<ProductVariant>())).Returns(productVariantDto);

        // act
        var result = await _productVariantService.GetProductVariantAsync(productId, productVariantId, false);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateProductVariantLabelAsync_Success()
    {
        // arrange  
        var productId = 1;
        var productVariantId = 1;

        var productVariantUpdateNameDto = new ProductVariantUpdateLabelDto
        {
            Label = "asd"
        };

        // act
        Func<Task> act = async () =>
        {
            await _productVariantService.UpdateProductVariantLabelAsync(productId, productVariantId,
                productVariantUpdateNameDto);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductVariantLabelAsync_Failed()
    {
        // arrange  
        var productId = 1;
        var productVariantId = 1;

        var productVariantUpdateNameDto = new ProductVariantUpdateLabelDto
        {
            Label = "asd"
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _productVariantService.UpdateProductVariantLabelAsync(productId, productVariantId,
                productVariantUpdateNameDto);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task DeleteProductVariantAsync_Success()
    {
        // arrange  
        var productId = 1;
        var productVariantId = 1;

        _repository.Setup(s => s.ProductVariant.DeleteProductVariant(
            It.IsAny<ProductVariant>()));

        // act
        Func<Task> act = async () =>
        {
            await _productVariantService.DeleteProductVariantAsync(productId, productVariantId, false);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteProductVariantAsync_Failed()
    {
        // arrange  
        var productId = 1;
        var productVariantId = 1;

        _repository.Setup(s => s.ProductVariant.DeleteProductVariant(
            It.IsAny<ProductVariant>()));

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _productVariantService.DeleteProductVariantAsync(productId, productVariantId, false);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }
}
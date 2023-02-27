using AutoMapper;
using Catalog.API.Exceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.UnitTests.Services;

public class CatalogProductServiceTest
{
    private readonly IProductService _productService;

    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILogger<ProductService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly Category _testCategory = new Category
    {
        Name = "test"
    };

    private readonly Consumer _testConsumer = new Consumer
    {
        Name = "test"
    };

    public CatalogProductServiceTest()
    {
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILogger<ProductService>>();
        _mapper = new Mock<IMapper>();

        _productService = new ProductService(_repository.Object, _logger.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateProductAsync_Success()
    {
        // arrange  
        var productForCreationDto = new ProductForCreationDto
        {
            Name = "test",
            ConsumerIds = new[] { 0 }
        };

        var productDto = new ProductDto
        {
            Name = "name"
        };

        var product = new Product
        {
            Name = "name"
        };

        _repository.Setup(s => s.Category.GetCategoryAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testCategory);

        _repository.Setup(s => s.Consumer.GetConsumerAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testConsumer);

        _repository.Setup(s => s.Product.CreateProductAsync(
            It.IsAny<Product>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<ProductDto>(
            It.IsAny<Product>())).Returns(productDto);

        _mapper.Setup(s => s.Map<Product>(
            It.IsAny<ProductForCreationDto>())).Returns(product);

        // act
        var result = await _productService.CreateProductAsync(productForCreationDto);

        // assert
        result.Name.Should().NotBeNull();
        result.Name.Should().Be(productDto.Name);
    }

    [Fact]
    public async Task CreateProductAsync_Failed()
    {
        // arrange  
        var productForCreationDto = new ProductForCreationDto
        {
            Name = "test",
            ConsumerIds = new[] { 0 }
        };

        var productDto = new ProductDto
        {
            Name = null
        };

        var product = new Product
        {
            Name = "name"
        };

        _repository.Setup(s => s.Category.GetCategoryAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testCategory);

        _repository.Setup(s => s.Consumer.GetConsumerAsync(
            It.IsAny<int>(),
            It.IsAny<bool>())).ReturnsAsync(_testConsumer);

        _repository.Setup(s => s.Product.CreateProductAsync(
            It.IsAny<Product>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<ProductDto>(
            It.IsAny<Product>())).Returns(productDto);

        _mapper.Setup(s => s.Map<Product>(
            It.IsAny<ProductForCreationDto>())).Returns(product);

        // act
        var result = await _productService.CreateProductAsync(productForCreationDto);

        // assert
        result.Name.Should().Be(productDto.Name);
    }

    [Fact]
    public async Task GetProductsAsync_Success()
    {
        // arrange  
        var productParameters = new ProductParameters();

        var productDtos = new List<ProductDto>
        {
            new()
            {
                Name = "test"
            }
        };

        var productEntities = new PagedList<Product>(new List<Product>
        {
            new Product
            {
                Name = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Product.GetProductsAsync(
            productParameters,
            false)).ReturnsAsync(productEntities);

        _mapper.Setup(s => s.Map<IEnumerable<ProductDto>>(
            It.IsAny<IEnumerable<Product>>())).Returns(productDtos);

        // act
        var result = await _productService.GetProductsAsync(productParameters, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductsAsync_Failed()
    {
        // arrange  
        var productParameters = new ProductParameters();

        List<ProductDto>? productDtos = null;

        var productEntities = new PagedList<Product>(new List<Product>
        {
            new Product
            {
                Name = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Product.GetProductsAsync(
            productParameters,
            false)).ReturnsAsync(productEntities);

        _mapper.Setup(s => s.Map<IEnumerable<ProductDto>>(
            It.IsAny<IEnumerable<Product>>())).Returns(productDtos);

        // act
        Func<Task> act = async () => { await _productService.GetProductsAsync(productParameters, false); };
        // assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetProductAsync_Success()
    {
        // arrange  
        var productDto = new ProductDto
        {
            Name = "test"
        };

        var productEntity = new Product
        {
            Name = "test"
        };

        var productId = 1;

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            false)).ReturnsAsync(productEntity);

        _mapper.Setup(s => s.Map<ProductDto>(
            It.IsAny<Product>())).Returns(productDto);

        // act
        var result = await _productService.GetProductAsync(productId, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductAsync_Failed()
    {
        // arrange  
        ProductDto? productDto = null;

        var productEntity = new Product
        {
            Name = "test"
        };

        var productId = 1;

        _repository.Setup(s => s.Product.GetProductAsync(
            It.IsAny<int>(),
            false)).ReturnsAsync(productEntity);

        _mapper.Setup(s => s.Map<ProductDto>(
            It.IsAny<Product>())).Returns(productDto);

        // act
        var result = await _productService.GetProductAsync(productId, false);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateProductNameAsync_Success()
    {
        // arrange  
        var productId = 1;

        var productUpdateNameDto = new ProductUpdateNameDto
        {
            Name = "asd"
        };

        // act
        Func<Task> act = async () => { await _productService.UpdateProductNameAsync(productId, productUpdateNameDto); };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductNameAsync_Failed()
    {
        // arrange  
        var productId = 1;

        var productUpdateNameDto = new ProductUpdateNameDto
        {
            Name = "asd"
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () => { await _productService.UpdateProductNameAsync(productId, productUpdateNameDto); };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task UpdateProductDescriptionAsync_Success()
    {
        // arrange  
        var productId = 1;

        var productUpdateDescriptionDto = new ProductUpdateDescriptionDto()
        {
            Description = "asd"
        };

        // act
        Func<Task> act = async () =>
        {
            await _productService.UpdateProductDescriptionAsync(productId, productUpdateDescriptionDto);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductDescriptionAsync_Failed()
    {
        // arrange  
        var productId = 1;

        var productUpdateDescriptionDto = new ProductUpdateDescriptionDto
        {
            Description = "asd"
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _productService.UpdateProductDescriptionAsync(productId, productUpdateDescriptionDto);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task UpdateProductPictureFIleNameAsync_Success()
    {
        // arrange  
        var productId = 1;

        var productUpdatePictureFileNameDto = new ProductUpdatePictureFileNameDto
        {
            PictureFileName = "asd"
        };

        // act
        Func<Task> act = async () =>
        {
            await _productService.UpdateProductPictureFileNameAsync(productId, productUpdatePictureFileNameDto);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductPictureFIleNameAsync_Failed()
    {
        // arrange  
        var productId = 1;

        var productUpdatePictureFileNameDto = new ProductUpdatePictureFileNameDto
        {
            PictureFileName = "asd"
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _productService.UpdateProductPictureFileNameAsync(productId, productUpdatePictureFileNameDto);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task UpdateProductCategoryAsync_Success()
    {
        // arrange  
        var productId = 1;

        var productUpdateCategoryDto = new ProductUpdateCategoryDto
        {
            CategoryId = 0
        };

        // act
        Func<Task> act = async () =>
        {
            await _productService.UpdateProductCategoryAsync(productId, productUpdateCategoryDto);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductCategoryAsync_Failed()
    {
        // arrange  
        var productId = 1;

        var productUpdateCategoryDto = new ProductUpdateCategoryDto
        {
            CategoryId = 0
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _productService.UpdateProductCategoryAsync(productId, productUpdateCategoryDto);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task UpdateProductAddConsumerAsync_Success()
    {
        // arrange  
        var productId = 1;

        var productUpdateConsumerDto = new ProductUpdateConsumerDto()
        {
            ConsumerId = 0
        };

        // act
        Func<Task> act = async () => { await _productService.UpdateProductAddConsumerAsync(productId, productUpdateConsumerDto); };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductAddConsumerAsync_Failed()
    {
        // arrange  
        var productId = 1;
        
        var productUpdateConsumerDto = new ProductUpdateConsumerDto()
        {
            ConsumerId = 0
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () => { await _productService.UpdateProductAddConsumerAsync(productId, productUpdateConsumerDto); };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task UpdateProductRemoveConsumerAsync_Success()
    {
        // arrange  
        var productId = 1;

        var productUpdateConsumerDto = new ProductUpdateConsumerDto()
        {
            ConsumerId = 0
        };

        // act
        Func<Task> act = async () => { await _productService.UpdateProductRemoveConsumerAsync(productId, productUpdateConsumerDto); };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProductRemoveConsumerAsync_Failed()
    {
        // arrange  
        var productId = 1;
        
        var productUpdateConsumerDto = new ProductUpdateConsumerDto()
        {
            ConsumerId = 0
        };

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () => { await _productService.UpdateProductRemoveConsumerAsync(productId, productUpdateConsumerDto); };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task DeleteProductAsync_Success()
    {
        // arrange  
        var productId = 1;

        _repository.Setup(s => s.Product.DeleteProduct(
            It.IsAny<Product>()));

        // act
        Func<Task> act = async () => { await _productService.DeleteProductAsync(productId, false); };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteProductAsync_Failed()
    {
        // arrange  
        var productId = 1;

        _repository.Setup(s => s.Product.DeleteProduct(
            It.IsAny<Product>()));

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () => { await _productService.DeleteProductAsync(productId, false); };
        // assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
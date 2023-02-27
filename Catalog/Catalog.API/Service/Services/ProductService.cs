using AutoMapper;
using Catalog.API.Exceptions.BadRequestExceptions;
using Catalog.API.Exceptions.NotFoundExceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repositoryManager, ILogger<ProductService> logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetProductsAsync(
        ProductParameters productParameters, bool trackChanges)
    {
        var productEntities = await _repository.Product.GetProductsAsync(productParameters, trackChanges);

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productEntities);

        if (productParameters.ValidPriceRange)
        {
            productDtos = productDtos.Where(p =>
                p.Price > productParameters.MinPrice && p.Price < productParameters.MaxPrice);
        }

        _logger.LogInformation(
            $"Returned products on page: {productParameters.PageIndex} with {productParameters.PageSize} elements");

        return (productDtos, productEntities.MetaData);
    }

    public async Task<ProductDto> GetProductAsync(int productId, bool trackChanges)
    {
        var productEntity = await GetProductIfExistsAsync(productId, trackChanges);

        var productDto = _mapper.Map<ProductDto>(productEntity);

        _logger.LogInformation($"Returned product with id: {productId}");

        return productDto;
    }

    public async Task<ProductDto> CreateProductAsync(ProductForCreationDto productForCreation)
    {
        await GetCategoryIfExists(productForCreation.CategoryId, trackChanges: false);
        await CheckIfConsumersExistAsync(productForCreation.ConsumerIds, trackChanges: false);

        var productEntity = _mapper.Map<Product>(productForCreation);

        await _repository.Product.CreateProductAsync(productEntity);
        await _repository.SaveAsync();

        productEntity.Consumers =
            (await _repository.Consumer.GetConsumersByIdsAsync(productForCreation.ConsumerIds, false)).ToList();
        await _repository.SaveAsync();

        var productDto = _mapper.Map<ProductDto>(productEntity);

        _logger.LogInformation($"Created product with id: {productDto.Id}");

        return productDto;
    }

    public async Task UpdateProductNameAsync(int productId, ProductUpdateNameDto productUpdateNameDto)
    {
        var product = await GetProductIfExistsAsync(productId, trackChanges: true);

        product.Name = productUpdateNameDto.Name;

        await _repository.SaveAsync();

        _logger.LogInformation($"Updated product with id: {productId} changed name to: {productUpdateNameDto.Name}");
    }

    public async Task UpdateProductDescriptionAsync(int productId,
        ProductUpdateDescriptionDto productUpdateDescriptionDto)
    {
        var product = await GetProductIfExistsAsync(productId, trackChanges: true);

        product.Description = productUpdateDescriptionDto.Description;

        await _repository.SaveAsync();

        _logger.LogInformation($"Updated product with id: {productId} description");
    }

    public async Task UpdateProductPictureFileNameAsync(int productId,
        ProductUpdatePictureFileNameDto productUpdatePictureFileNameDto)
    {
        var product = await GetProductIfExistsAsync(productId, trackChanges: true);

        product.PictureFileName = productUpdatePictureFileNameDto.PictureFileName;

        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product with id: {productId} picture file name to {productUpdatePictureFileNameDto.PictureFileName}");
    }

    public async Task UpdateProductCategoryAsync(int productId, ProductUpdateCategoryDto productUpdateCategoryDto)
    {
        var category = await GetCategoryIfExists(productUpdateCategoryDto.CategoryId, trackChanges: false);
        var product = await GetProductIfExistsAsync(productId, trackChanges: true);

        product.CategoryId = productUpdateCategoryDto.CategoryId;
        product.Category = category;

        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product with id: {productId} changed category to category with id: {category.Id}");
    }

    public async Task UpdateProductAddConsumerAsync(int productId, ProductUpdateConsumerDto productUpdateConsumerDto)
    {
        var product = await GetProductIfExistsAsync(productId, trackChanges: true);
        var consumer = await GetConsumerIfExistAsync(productUpdateConsumerDto.ConsumerId, false);

        if (product.Consumers.Contains(consumer))
        {
            throw new ConsumerAlreadyAttachedBadRequestException(productId, productUpdateConsumerDto.ConsumerId);
        }

        product.Consumers.Add(consumer);

        await _repository.SaveAsync();

        _logger.LogInformation($"Updated product with id: {productId} added consumer with id: {consumer.Id}");
    }

    public async Task UpdateProductRemoveConsumerAsync(int productId, ProductUpdateConsumerDto productUpdateConsumerDto)
    {
        var product = await GetProductIfExistsAsync(productId, trackChanges: true);

        var consumer = product.Consumers.SingleOrDefault(c => c.Id.Equals(productUpdateConsumerDto.ConsumerId));

        if (consumer is null)
        {
            throw new ConsumerNotAttachedBadRequestException(productId, productUpdateConsumerDto.ConsumerId);
        }

        product.Consumers.Remove(consumer);

        await _repository.SaveAsync();

        _logger.LogInformation($"Updated product with id: {productId} removed consumer with id: {consumer.Id}");
    }

    public async Task DeleteProductAsync(int productId, bool trackChanges)
    {
        var product = await GetProductIfExistsAsync(productId, trackChanges);

        _repository.Product.DeleteProduct(product);
        await _repository.SaveAsync();

        _logger.LogInformation($"Deleted product with id: {productId}");
    }

    private async Task<Product> GetProductIfExistsAsync(int productId, bool trackChanges)
    {
        var productEntity = await _repository.Product.GetProductAsync(productId, trackChanges);

        if (productEntity is null)
        {
            throw new ProductNotFoundException(productId);
        }

        return productEntity;
    }

    private async Task<Category> GetCategoryIfExists(int categoryId, bool trackChanges)
    {
        var categoryEntity = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);

        if (categoryEntity is null)
        {
            throw new CategoryNotFoundException(categoryId);
        }

        return categoryEntity;
    }

    private async Task<Consumer> GetConsumerIfExistAsync(int consumerId, bool trackChanges)
    {
        var consumerEntity = await _repository.Consumer.GetConsumerAsync(consumerId, trackChanges);

        if (consumerEntity is null)
        {
            throw new ConsumerNotFoundException(consumerId);
        }

        return consumerEntity;
    }

    private async Task CheckIfConsumersExistAsync(IEnumerable<int> consumerIds, bool trackChanges)
    {
        foreach (var consumerId in consumerIds)
        {
            await GetConsumerIfExistAsync(consumerId, trackChanges);
        }
    }
}
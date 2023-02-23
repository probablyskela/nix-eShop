using AutoMapper;
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

        return (productDtos, productEntities.MetaData);
    }

    public async Task<ProductDto> GetProductAsync(int productId, bool trackChanges)
    {
        var productEntity = await GetProductIfExistsAsync(productId, trackChanges);

        var productDto = _mapper.Map<ProductDto>(productEntity);

        return productDto;
    }

    public async Task<ProductDto> CreateProductAsync(ProductForCreationDto productForCreation)
    {
        var productEntity = _mapper.Map<Product>(productForCreation);

        await CheckIfCategoryExistsAsync(productForCreation.CategoryId, trackChanges: false);
        await CheckIfPictureExistsAsync(productForCreation.PictureId, trackChanges: false);
        await CheckIfConsumersExistAsync(productForCreation.ConsumerIds, trackChanges: false);

        await _repository.Product.CreateProductAsync(productEntity);
        await _repository.SaveAsync();

        var productDto = _mapper.Map<ProductDto>(productEntity);

        return productDto;
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

    private async Task CheckIfPictureExistsAsync(int pictureId, bool trackChanges)
    {
        var pictureEntity = await _repository.Picture.GetPictureAsync(pictureId, trackChanges);

        if (pictureEntity is null)
        {
            throw new PictureNotFoundException(pictureId);
        }
    }

    private async Task CheckIfCategoryExistsAsync(int categoryId, bool trackChanges)
    {
        var categoryEntity = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);

        if (categoryEntity is null)
        {
            throw new CategoryNotFoundException(categoryId);
        }
    }

    private async Task CheckIfConsumersExistAsync(IEnumerable<int> consumerIds, bool trackChanges)
    {
        foreach (var consumerId in consumerIds)
        {
            var consumerEntity = await _repository.Consumer.GetConsumerAsync(consumerId, trackChanges);

            if (consumerEntity is null)
            {
                throw new ConsumerNotFoundException(consumerId);
            }
        }
    }
}
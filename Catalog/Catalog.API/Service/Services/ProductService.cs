using AutoMapper;
using Catalog.API.Exceptions.NotFoundExceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repositoryManager, ILogger<ProductService> logger,
        IMapper mapper)
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

    private async Task<Product> GetProductIfExistsAsync(int productId, bool trackChanges)
    {
        var productEntity = await _repository.Product.GetProductAsync(productId, trackChanges);

        if (productEntity is null)
        {
            throw new ProductNotFoundException(productId);
        }

        return productEntity;
    }
}
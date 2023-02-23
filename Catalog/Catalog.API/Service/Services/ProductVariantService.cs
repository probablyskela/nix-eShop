using AutoMapper;
using Catalog.API.Exceptions.NotFoundExceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services;

public class ProductVariantService : IProductVariantService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<ProductVariantService> _logger;
    private readonly IMapper _mapper;

    public ProductVariantService(IRepositoryManager repositoryManager,
        ILogger<ProductVariantService> logger,
        IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<ProductVariantDto> productVariantDtos, MetaData metaData)> GetProductVariantsAsync(
        int productId, ProductVariantParameters productVariantParameters, bool trackChanges)
    {
        await CheckIfProductExistsAsync(productId, trackChanges: false);

        var productVariantEntities =
            await _repository.ProductVariant.GetProductVariantsAsync(productId, productVariantParameters,
                trackChanges);

        var productVariantDtos = _mapper.Map<IEnumerable<ProductVariantDto>>(productVariantEntities);

        return (productVariantDtos, productVariantEntities.MetaData);
    }

    public async Task<ProductVariantDto> GetProductVariantAsync(int productId, int productVariantId, bool trackChanges)
    {
        await CheckIfProductExistsAsync(productId, trackChanges: false);

        var productVariantEntity = GetProductVariantIfExists(productId, productVariantId, trackChanges);

        var productVariantDto = _mapper.Map<ProductVariantDto>(productVariantEntity);

        return productVariantDto;
    }

    public async Task<ProductVariantDto> CreateProductVariantAsync(int productId,
        ProductVariantForCreationDto productVariantForCreation)
    {
        await CheckIfProductExistsAsync(productId, trackChanges: false);
        await CheckIfPictureExistsAsync(productVariantForCreation.PictureIds, trackChanges: false);

        var productVariantEntity = _mapper.Map<ProductVariant>(productVariantForCreation);
        productVariantEntity.ProductId = productId;

        await _repository.ProductVariant.CreateProductVariantAsync(productVariantEntity);
        await _repository.SaveAsync();

        var productVariantDto = _mapper.Map<ProductVariantDto>(productVariantEntity);

        return productVariantDto;
    }

    private async Task CheckIfProductExistsAsync(int productId, bool trackChanges)
    {
        var productEntity = await _repository.Product.GetProductAsync(productId, trackChanges);

        if (productEntity is null)
        {
            throw new ProductNotFoundException(productId);
        }
    }

    private async Task<ProductVariant> GetProductVariantIfExists(int productId, int productVariantId, bool trackChanges)
    {
        var productVariantEntity =
            await _repository.ProductVariant.GetProductVariantAsync(productId, productVariantId, trackChanges);

        if (productVariantEntity is null)
        {
            throw new ProductVariantNotFoundException(productId, productVariantId);
        }

        return productVariantEntity;
    }

    private async Task CheckIfPictureExistsAsync(IEnumerable<int> pictureIds, bool trackChanges)
    {
        foreach (var pictureId in pictureIds)
        {
            var pictureEntity = await _repository.Picture.GetPictureAsync(pictureId, trackChanges);

            if (pictureEntity is null)
            {
                throw new PictureNotFoundException(pictureId);
            }
        }
    }
}
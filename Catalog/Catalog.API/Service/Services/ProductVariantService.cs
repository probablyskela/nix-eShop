using AutoMapper;
using Catalog.API.Exceptions.BadRequestExceptions;
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

        _logger.LogInformation(
            $"Returned product variants for product with id: {productId} on page: {productVariantParameters.PageIndex} with {productVariantParameters.PageSize} elements");

        return (productVariantDtos, productVariantEntities.MetaData);
    }

    public async Task<ProductVariantDto> GetProductVariantAsync(int productId, int productVariantId, bool trackChanges)
    {
        await CheckIfProductExistsAsync(productId, trackChanges: false);

        var productVariantEntity = await GetProductVariantIfExistsAsync(productId, productVariantId, trackChanges);

        var productVariantDto = _mapper.Map<ProductVariantDto>(productVariantEntity);

        _logger.LogInformation(
            $"Returned product variant with id: {productVariantId} for product with id: {productId}");

        return productVariantDto;
    }

    public async Task<ProductVariantDto> CreateProductVariantAsync(int productId,
        ProductVariantForCreationDto productVariantForCreation)
    {
        await CheckIfProductExistsAsync(productId, trackChanges: false);

        var productVariantEntity = _mapper.Map<ProductVariant>(productVariantForCreation);
        productVariantEntity.ProductId = productId;

        await _repository.ProductVariant.CreateProductVariantAsync(productVariantEntity);
        await _repository.SaveAsync();

        foreach (var pictureFileName in productVariantForCreation.PictureFileNames)
        {
            var productVariantPictureEntity = new ProductVariantPicture
                { ProductVariantId = productVariantEntity.Id, PictureFileName = pictureFileName };

            await _repository.ProductVariantPicture.CreateProductVariantPictureAsync(productVariantPictureEntity);
        }

        await _repository.SaveAsync();

        var productVariantDto = _mapper.Map<ProductVariantDto>(productVariantEntity);

        _logger.LogInformation(
            $"Created product variant with id: {productVariantDto.Id} for product with id: {productId}");

        return productVariantDto;
    }

    public async Task UpdateProductVariantLabelAsync(int productId, int productVariantId,
        ProductVariantUpdateLabelDto productVariantUpdateLabelDto)
    {
        await CheckIfProductExistsAsync(productId, false);
        var productVariant = await GetProductVariantIfExistsAsync(productId, productVariantId, trackChanges: true);

        productVariant.Label = productVariantUpdateLabelDto.Label;

        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product variant with id: {productVariantId} for product with id: {productId} label to: {productVariantUpdateLabelDto.Label}");
    }

    public async Task UpdateProductVariantPriceAsync(int productId, int productVariantId,
        ProductVariantUpdatePriceDto productVariantUpdatePriceDto)
    {
        await CheckIfProductExistsAsync(productId, false);
        var productVariant = await GetProductVariantIfExistsAsync(productId, productVariantId, trackChanges: true);

        productVariant.Price = productVariantUpdatePriceDto.Price;

        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product variant with id: {productVariantId} for product with id: {productId} price to: {productVariantUpdatePriceDto.Price}");
    }

    public async Task UpdateProductVariantAvailableStockAsync(int productId, int productVariantId,
        ProductVariantUpdateAvailableStockDto productVariantUpdateAvailableStockDto)
    {
        await CheckIfProductExistsAsync(productId, false);
        var productVariant = await GetProductVariantIfExistsAsync(productId, productVariantId, trackChanges: true);

        productVariant.AvailableStock = productVariantUpdateAvailableStockDto.AvailableStock;

        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product variant with id: {productVariantId} for product with id: {productId} available stock to: {productVariantUpdateAvailableStockDto.AvailableStock}");
    }

    public async Task UpdateProductVariantAddPictureAsync(int productId, int productVariantId,
        ProductVariantUpdatePictureFileNameDto productVariantUpdatePictureFileNameDto)
    {
        var productVariant = await GetProductVariantIfExistsAsync(productId, productVariantId, true);
        var picture = productVariant.ProductVariantPictures
            .SingleOrDefault(p => p.PictureFileName
                .Equals(productVariantUpdatePictureFileNameDto.PictureFileName));

        if (picture is not null)
        {
            throw new PictureAlreadyAttachedBadRequestException(productId, productVariantId,
                productVariantUpdatePictureFileNameDto.PictureFileName);
        }

        var productVariantPictureEntity = new ProductVariantPicture
        {
            ProductVariantId = productVariant.Id,
            PictureFileName = productVariantUpdatePictureFileNameDto.PictureFileName
        };

        await _repository.ProductVariantPicture.CreateProductVariantPictureAsync(productVariantPictureEntity);
        await _repository.SaveAsync();

        productVariant.ProductVariantPictures.Add(productVariantPictureEntity);
        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product variant with id: {productVariantId} for product with id: {productId} added picture with name: {productVariantUpdatePictureFileNameDto.PictureFileName}");
    }

    public async Task UpdateProductVariantRemovePictureAsync(int productId, int productVariantId,
        ProductVariantUpdatePictureFileNameDto productVariantUpdatePictureFileNameDto)
    {
        var productVariant = await GetProductVariantIfExistsAsync(productId, productVariantId, true);
        var picture = productVariant.ProductVariantPictures
            .SingleOrDefault(p => p.PictureFileName
                .Equals(productVariantUpdatePictureFileNameDto.PictureFileName));

        if (picture is null)
        {
            throw new PictureNotAttachedBadRequestException(productId, productVariantId,
                productVariantUpdatePictureFileNameDto.PictureFileName);
        }

        productVariant.ProductVariantPictures.Remove(picture);
        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Updated product variant with id: {productVariantId} for product with id: {productId} removed picture with name: {productVariantUpdatePictureFileNameDto.PictureFileName}");
    }

    public async Task DeleteProductVariantAsync(int productId, int productVariantId, bool trackChanges)
    {
        await CheckIfProductExistsAsync(productId, trackChanges);
        var productVariant = await GetProductVariantIfExistsAsync(productId, productVariantId, trackChanges);

        _repository.ProductVariant.DeleteProductVariant(productVariant);
        await _repository.SaveAsync();

        _logger.LogInformation(
            $"Deleted product variant with id: {productVariantId} for product with id: {productId}");
    }

    private async Task CheckIfProductExistsAsync(int productId, bool trackChanges)
    {
        var productEntity = await _repository.Product.GetProductAsync(productId, trackChanges);

        if (productEntity is null)
        {
            throw new ProductNotFoundException(productId);
        }
    }

    private async Task<ProductVariant> GetProductVariantIfExistsAsync(int productId, int productVariantId,
        bool trackChanges)
    {
        var productVariantEntity =
            await _repository.ProductVariant.GetProductVariantAsync(productId, productVariantId, trackChanges);

        if (productVariantEntity is null)
        {
            throw new ProductVariantNotFoundException(productId, productVariantId);
        }

        return productVariantEntity;
    }
}
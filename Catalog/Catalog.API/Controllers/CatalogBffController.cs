using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared;
using Shared.Data.Dtos.CategoryDto;
using Shared.Data.Dtos.ConsumerDtos;
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Data.Responses;
using Shared.Identity;

namespace Catalog.API.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute + "/[controller]")]
public class CatalogBffController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogBffController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("Products")]
    public async Task<IActionResult> Products([FromQuery] ProductParameters productParameters)
    {
        var products = await _service.Product.GetProductsAsync(productParameters, trackChanges: false);

        return Ok(new PaginatedResponse<ProductDto>(products.productDtos, products.metaData));
    }

    [HttpPost("Products/{productId:int}")]
    public async Task<IActionResult> Product([FromRoute] int productId)
    {
        var product = await _service.Product.GetProductAsync(productId, trackChanges: false);

        return Ok(product);
    }

    [HttpPost("Products/{productId:int}/ProductVariants")]
    public async Task<IActionResult> ProductVariants([FromRoute] int productId,
        [FromQuery] ProductVariantParameters productVariantParameters)
    {
        var productVariants =
            await _service.ProductVariant.GetProductVariantsAsync(productId, productVariantParameters,
                trackChanges: false);

        return Ok(
            new PaginatedResponse<ProductVariantDto>(productVariants.productVariantDtos, productVariants.metaData));
    }

    [HttpPost("Products/{productId:int}/ProductVariants/{productVariantId:int}")]
    public async Task<IActionResult> ProductVariant([FromRoute] int productId, [FromRoute] int productVariantId)
    {
        var productVariant =
            await _service.ProductVariant.GetProductVariantAsync(productId, productVariantId, trackChanges: false);

        return Ok(productVariant);
    }

    [HttpPost("Categories/")]
    public async Task<IActionResult> Categories([FromQuery] CategoryParameters categoryParameters)
    {
        var categories = await _service.Category.GetCategoriesAsync(categoryParameters, trackChanges: false);

        return Ok(new PaginatedResponse<CategoryDto>(categories.categoryDtos, categories.metaData));
    }

    [HttpPost("Categories/{categoryId:int}")]
    public async Task<IActionResult> Category([FromRoute] int categoryId)
    {
        var category = await _service.Category.GetCategoryAsync(categoryId, trackChanges: false);

        return Ok(category);
    }

    [HttpPost("Consumers/")]
    public async Task<IActionResult> Consumers([FromQuery] ConsumerParameters consumerParameters)
    {
        var consumers = await _service.Consumer.GetConsumersAsync(consumerParameters, trackChanges: false);

        return Ok(new PaginatedResponse<ConsumerDto>(consumers.consumerDtos, consumers.metaData));
    }

    [HttpPost("Consumers/{consumerId:int}")]
    public async Task<IActionResult> Consumer([FromRoute] int consumerId)
    {
        var consumer = await _service.Consumer.GetConsumerAsync(consumerId, trackChanges: false);

        return Ok(consumer);
    }
}
using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.ProductDtos;

namespace Catalog.API.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute + "/Products/[action]")]
public class CatalogProductController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogProductController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto productForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var product = await _service.Product.CreateProductAsync(productForCreation);

        return Ok(product);
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> UpdateProductName([FromRoute] int productId,
        [FromBody] ProductUpdateNameDto productUpdateNameDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Product.UpdateProductNameAsync(productId, productUpdateNameDto);

        return NoContent();
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> UpdateProductDescription([FromRoute] int productId,
        [FromBody] ProductUpdateDescriptionDto productUpdateDescriptionDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Product.UpdateProductDescriptionAsync(productId, productUpdateDescriptionDto);

        return NoContent();
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> UpdateProductPictureFileName([FromRoute] int productId,
        [FromBody] ProductUpdatePictureFileNameDto productUpdatePictureFileNameDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Product.UpdateProductPictureFileNameAsync(productId, productUpdatePictureFileNameDto);

        return NoContent();
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> UpdateProductCategory([FromRoute] int productId,
        [FromBody] ProductUpdateCategoryDto productUpdateCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Product.UpdateProductCategoryAsync(productId, productUpdateCategoryDto);

        return NoContent();
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> UpdateProductAddConsumer([FromRoute] int productId,
        [FromBody] ProductUpdateConsumerDto productUpdateConsumerDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Product.UpdateProductAddConsumerAsync(productId, productUpdateConsumerDto);

        return NoContent();
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> UpdateProductRemoveConsumer([FromRoute] int productId,
        [FromBody] ProductUpdateConsumerDto productUpdateConsumerDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Product.UpdateProductRemoveConsumerAsync(productId, productUpdateConsumerDto);

        return NoContent();
    }

    [HttpPost("{productId:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
    {
        await _service.Product.DeleteProductAsync(productId, trackChanges: false);

        return NoContent();
    }
}
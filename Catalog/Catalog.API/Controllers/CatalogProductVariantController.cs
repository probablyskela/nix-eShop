using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Identity;

namespace Catalog.API.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogProduct")]
[Route(ComponentDefaults.DefaultRoute + "/Products/{productId:int}/ProductVariants/[action]")]
public class CatalogProductVariantController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogProductVariantController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductVariant([FromRoute] int productId,
        [FromBody] ProductVariantForCreationDto productVariantForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var productVariant =
            await _service.ProductVariant.CreateProductVariantAsync(productId, productVariantForCreation);

        return Ok(productVariant);
    }

    [HttpPost("{productVariantId:int}")]
    public async Task<IActionResult> UpdateProductVariantLabel([FromRoute] int productId,
        [FromRoute] int productVariantId,
        [FromBody] ProductVariantUpdateLabelDto productVariantUpdateLabelDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.ProductVariant.UpdateProductVariantLabelAsync(productId, productVariantId,
            productVariantUpdateLabelDto);

        return NoContent();
    }

    [HttpPost("{productVariantId:int}")]
    public async Task<IActionResult> UpdateProductVariantPrice([FromRoute] int productId,
        [FromRoute] int productVariantId,
        [FromBody] ProductVariantUpdatePriceDto productVariantUpdatePriceDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.ProductVariant.UpdateProductVariantPriceAsync(productId, productVariantId,
            productVariantUpdatePriceDto);

        return NoContent();
    }

    [HttpPost("{productVariantId:int}")]
    public async Task<IActionResult> UpdateProductVariantAvailableStock([FromRoute] int productId,
        [FromRoute] int productVariantId,
        [FromBody] ProductVariantUpdateAvailableStockDto productVariantUpdateAvailableStockDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.ProductVariant.UpdateProductVariantAvailableStockAsync(productId, productVariantId,
            productVariantUpdateAvailableStockDto);

        return NoContent();
    }

    [HttpPost("{productVariantId:int}")]
    public async Task<IActionResult> UpdateProductVariantAddPicture([FromRoute] int productId,
        [FromRoute] int productVariantId,
        [FromBody] ProductVariantUpdatePictureFileNameDto productVariantUpdatePictureFileNameDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.ProductVariant.UpdateProductVariantAddPictureAsync(productId, productVariantId,
            productVariantUpdatePictureFileNameDto);

        return NoContent();
    }

    [HttpPost("{productVariantId:int}")]
    public async Task<IActionResult> UpdateProductVariantRemovePicture([FromRoute] int productId,
        [FromRoute] int productVariantId,
        [FromBody] ProductVariantUpdatePictureFileNameDto productVariantUpdatePictureFileNameDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.ProductVariant.UpdateProductVariantRemovePictureAsync(productId, productVariantId,
            productVariantUpdatePictureFileNameDto);

        return NoContent();
    }

    [HttpPost("{productVariantId:int}")]
    public async Task<IActionResult> DeleteProductVariant([FromRoute] int productId, [FromRoute] int productVariantId)
    {
        await _service.ProductVariant.DeleteProductVariant(productId, productVariantId, trackChanges: false);

        return NoContent();
    }
}
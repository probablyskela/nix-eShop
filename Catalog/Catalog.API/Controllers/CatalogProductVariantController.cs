using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.ProductVariantDtos;

namespace Catalog.API.Controllers;

[ApiController]
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
}
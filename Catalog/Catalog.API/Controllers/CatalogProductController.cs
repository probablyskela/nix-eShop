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
}
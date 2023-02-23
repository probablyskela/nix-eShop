using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.CategoryDto;

namespace Catalog.API.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute + "/Categories/[action]")]
public class CatalogCategoryController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogCategoryController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryVariant([FromBody] CategoryForCreationDto categoryForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var category = await _service.Category.CreateCategoryAsync(categoryForCreation);

        return Ok(category);
    }
}
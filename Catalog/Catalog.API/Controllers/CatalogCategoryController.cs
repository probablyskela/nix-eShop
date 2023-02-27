using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.CategoryDto;
using Shared.Identity;

namespace Catalog.API.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogCategory")]
[Route(ComponentDefaults.DefaultRoute + "/Categories/[action]")]
public class CatalogCategoryController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogCategoryController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto categoryForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var category = await _service.Category.CreateCategoryAsync(categoryForCreation);

        return Ok(category);
    }

    [HttpPost("{categoryId:int}")]
    public async Task<IActionResult> UpdateCategoryName([FromRoute] int categoryId, [FromBody] CategoryUpdateNameDto categoryUpdateNameDto)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Category.UpdateCategoryNameAsync(categoryId, categoryUpdateNameDto);

        return NoContent();
    }

    [HttpPost("{categoryId:int}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        await _service.Category.DeleteCategoryAsync(categoryId, trackChanges: false);
        
        return NoContent();
    }
}
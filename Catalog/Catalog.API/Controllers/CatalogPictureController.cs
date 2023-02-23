using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.PictureDtos;

namespace Catalog.API.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute + "/Pictures/[action]")]
public class CatalogPictureController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogPictureController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePicture([FromForm] PictureForCreationDto pictureForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var picture = await _service.Picture.CreatePictureAsync(pictureForCreation);

        return Ok(picture);
    }
}
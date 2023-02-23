using Catalog.API.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Data.Dtos.ConsumerDtos;

namespace Catalog.API.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute + "/Consumers/[action]")]
public class CatalogConsumerController : ControllerBase
{
    private readonly IServiceManager _service;

    public CatalogConsumerController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateConsumerVariant([FromBody] ConsumerForCreationDto consumerForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var consumer = await _service.Consumer.CreateConsumerAsync(consumerForCreation);

        return Ok(consumer);
    }
}
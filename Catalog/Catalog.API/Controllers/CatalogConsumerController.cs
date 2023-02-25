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
    public async Task<IActionResult> CreateConsumer([FromBody] ConsumerForCreationDto consumerForCreation)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var consumer = await _service.Consumer.CreateConsumerAsync(consumerForCreation);

        return Ok(consumer);
    }
    
    [HttpPost("{consumerId:int}")]
    public async Task<IActionResult> UpdateCategoryName([FromRoute] int consumerId, [FromBody] ConsumerUpdateNameDto consumerUpdateName)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.Consumer.UpdateConsumerNameAsync(consumerId, consumerUpdateName);

        return NoContent();
    }
    
    [HttpPost("{consumerId:int}")]
    public async Task<IActionResult> DeleteConsumer([FromRoute] int consumerId)
    {
        await _service.Consumer.DeleteConsumerAsync(consumerId, trackChanges: false);
        
        return NoContent();
    }
}
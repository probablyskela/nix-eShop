namespace Catalog.API.Exceptions.BadRequestExceptions;

public class ConsumerNotAttachedBadRequestException : BadRequestException
{
    public ConsumerNotAttachedBadRequestException(int productId, int consumerId)
        : base($"Product with id: {productId} doesn't have consumer with id: {consumerId} attached.")
    {
    }
}
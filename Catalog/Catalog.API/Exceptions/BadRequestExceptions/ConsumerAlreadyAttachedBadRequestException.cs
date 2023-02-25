namespace Catalog.API.Exceptions.BadRequestExceptions;

public class ConsumerAlreadyAttachedBadRequestException : BadRequestException
{
    public ConsumerAlreadyAttachedBadRequestException(int productId, int consumerId)
        : base($"Consumer with id: {consumerId} already attached to product with id: {productId}")
    {
    }
}
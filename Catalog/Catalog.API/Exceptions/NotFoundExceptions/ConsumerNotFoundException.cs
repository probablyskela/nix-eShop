namespace Catalog.API.Exceptions.NotFoundExceptions;

public class ConsumerNotFoundException : NotFoundException
{
    public ConsumerNotFoundException(int consumerId)
        : base($"Consumer with id: {consumerId} was not found.")
    {
    }
}
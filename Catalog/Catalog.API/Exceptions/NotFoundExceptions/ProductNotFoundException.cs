namespace Catalog.API.Exceptions.NotFoundExceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int productId) 
        : base($"The product with id: {productId} was not found.")
    {
    }
}
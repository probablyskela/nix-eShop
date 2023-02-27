namespace Catalog.API.Exceptions.NotFoundExceptions;

public class ProductVariantNotFoundException : NotFoundException
{
    public ProductVariantNotFoundException(int productId, int productVariantId)
        : base($"The product variant with id: {productVariantId} was not found in product with id: {productId}.")
    {
    }
}
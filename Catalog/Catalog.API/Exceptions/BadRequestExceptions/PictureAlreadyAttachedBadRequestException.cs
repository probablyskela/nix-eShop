namespace Catalog.API.Exceptions.BadRequestExceptions;

public class PictureAlreadyAttachedBadRequestException : BadRequestException
{
    public PictureAlreadyAttachedBadRequestException(int productId, int productVariantId, string pictureFileName)
        : base(
            $"Picture with file name: {pictureFileName} already attached to product variant with id: {productVariantId} in product with id: {productId}")
    {
    }
}
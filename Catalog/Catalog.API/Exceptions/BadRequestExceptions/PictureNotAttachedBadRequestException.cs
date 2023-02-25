namespace Catalog.API.Exceptions.BadRequestExceptions;

public class PictureNotAttachedBadRequestException : BadRequestException
{
    public PictureNotAttachedBadRequestException(int productId, int productVariantId, string pictureFileName)
        : base(
            $"Picture with file name: {pictureFileName} is not attached to product variant with id: {productVariantId} in product with id: {productId}")
    {
    }
}
namespace Catalog.API.Exceptions.NotFoundExceptions;

public class PictureNotFoundException : NotFoundException
{
    public PictureNotFoundException(int pictureId)
        : base($"The picture with id: {pictureId} was not found.")
    {
    }
}
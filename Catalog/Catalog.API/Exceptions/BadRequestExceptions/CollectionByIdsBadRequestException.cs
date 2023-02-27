namespace Catalog.API.Exceptions.BadRequestExceptions;

public sealed class CollectionByIdsBadRequestException : BadRequestException
{
    public CollectionByIdsBadRequestException()
        : base("Collection count mismatch comparing to ids")
    {
    }
}
namespace Catalog.API.Exceptions.NotFoundExceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(int categoryId)
        : base($"The category with id: {categoryId} was not found.")
    {
    }
}
using Catalog.API.Repository.Repositories.Abstractions;

namespace Catalog.API.Repository.Abstractions;

public interface IRepositoryManager
{
    IConsumerRepository Consumer { get; }
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    IProductVariantRepository ProductVariant { get; }
    IProductVariantPictureRepository ProductVariantPicture { get; }
    Task SaveAsync();
}
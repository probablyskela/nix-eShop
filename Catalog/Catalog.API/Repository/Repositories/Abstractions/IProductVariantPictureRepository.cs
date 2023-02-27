using Shared.Data.Entities;

namespace Catalog.API.Repository.Repositories.Abstractions;

public interface IProductVariantPictureRepository
{
    Task<ProductVariantPicture?> GetProductVariantPictureAsync(int productVariantId, string pictureFIleName,
        bool trackChanges);

    Task CreateProductVariantPictureAsync(ProductVariantPicture productVariantPicture);
    void DeleteProductVariantPicture(ProductVariantPicture productVariantPicture);
}
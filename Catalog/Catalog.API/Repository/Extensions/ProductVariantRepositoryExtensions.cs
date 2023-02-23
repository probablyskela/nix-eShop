using System.Linq.Dynamic.Core;
using Shared.Data.Entities;

namespace Catalog.API.Repository.Extensions;

public static class ProductVariantRepositoryExtensions
{
    public static IQueryable<ProductVariant> FilterPrice(this IQueryable<ProductVariant> productVariants, uint minPrice,
        uint maxPrice, bool validRange)
    {
        return !validRange ? productVariants : productVariants.Where(p => p.Price > minPrice && p.Price < maxPrice);
    }
}
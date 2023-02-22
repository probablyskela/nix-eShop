using Shared.Data.Entities;
using System.Linq.Dynamic.Core;
using Shared.Utils;

namespace Catalog.API.Repository.Extensions;

public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> Search(this IQueryable<Product> products, string? searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return products;
        }

        var lowerCaseTrimmedTerm = searchTerm.ToLower().Trim();

        return products.Where(e =>
            e.Name.ToLower().Contains(lowerCaseTrimmedTerm) || e.Description.ToLower().Contains(lowerCaseTrimmedTerm));
    }

    public static IQueryable<Product> FilterConsumers(this IQueryable<Product> products, IEnumerable<string>? consumers)
    {
        if (consumers is null)
        {
            return products;
        }

        var consumersString = string.Join(',', consumers).ToLowerInvariant();

        return products.Where(p => p.Consumers.Any(c => consumersString.Contains(c.Name.ToLower())));
    }
}
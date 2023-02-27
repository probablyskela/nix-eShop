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

    public static IQueryable<Product> FilterConsumers(this IQueryable<Product> products, IEnumerable<int>? consumers)
    {
        return consumers is null ? products : products.Where(p => p.Consumers.Any(c => consumers.Contains(c.Id)));
    }
}
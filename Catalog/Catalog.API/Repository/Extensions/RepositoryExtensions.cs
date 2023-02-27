using System.Linq.Dynamic.Core;
using Shared.Utils;

namespace Catalog.API.Repository.Extensions;

public static class RepositoryExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> entities, string? orderByQueryString = null)
    {
        if (string.IsNullOrEmpty(orderByQueryString))
        {
            return entities.OrderBy("Id ascending");
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<T>(orderByQueryString);

        return string.IsNullOrWhiteSpace(orderQuery) ? entities.OrderBy("Id ascending") : entities.OrderBy(orderQuery);
    }
}
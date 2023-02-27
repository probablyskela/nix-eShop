using Microsoft.EntityFrameworkCore;

namespace Shared.Misc;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PagedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        MetaData = new MetaData()
        {
            TotalCount = count,
            PageSize = pageSize,
            PageIndex = pageIndex,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
        };

        AddRange(items);
    }

    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageIndex, pageSize);
    }
}
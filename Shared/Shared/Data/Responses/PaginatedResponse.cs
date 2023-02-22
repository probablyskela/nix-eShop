using Shared.Data.Dtos.ProductDtos;
using Shared.Misc;

namespace Shared.Data.Responses;

public class PaginatedResponse<T>
{
    public PaginatedResponse(IEnumerable<T> dtos, MetaData metaData)
    {
        Data = dtos;
        PageIndex = metaData.PageIndex;
        TotalPages = metaData.TotalPages;
        PageSize = metaData.PageSize;
        TotalCount = metaData.TotalCount;
    }

    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }

    public bool HasPrevious => PageIndex > 1;
    public bool HasNext => PageIndex < TotalPages;
    public IEnumerable<T> Data { get; init; } = null!;
}
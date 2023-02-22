using Shared.Data.Dtos.CategoryDto;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services.Abstractions;

public interface ICategoryService
{
    Task<(IEnumerable<CategoryDto> categoryDtos, MetaData metaData)> GetCategoriesAsync(
        CategoryParameters productParameters, bool trackChanges);
    
    Task<CategoryDto> GetCategoryAsync(int categoryId, bool trackChanges);
}
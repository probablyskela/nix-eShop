using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Repository.Repositories.Abstractions;

public interface ICategoryRepository
{
    Task<PagedList<Category>> GetCategoriesAsync(CategoryParameters productParameters, bool trackChanges);
    Task<Category?> GetCategoryAsync(int categoryId, bool trackChanges);
    Task CreateCategoryAsync(Category category);
}
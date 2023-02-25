using Catalog.API.Repository.Extensions;
using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Category>> GetCategoriesAsync(CategoryParameters productParameters, bool trackChanges)
    {
        var categories = FindAll(trackChanges)
            .Include(c => c.Products)
            .Sort(productParameters.OrderBy);

        return await PagedList<Category>
            .ToPagedList(categories, productParameters.PageIndex, productParameters.PageSize);
    }

    public async Task<Category?> GetCategoryAsync(int categoryId, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .Include(c => c.Products)
            .SingleOrDefaultAsync();
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await CreateAsync(category);
    }

    public void DeleteCategory(Category category)
    {
        Delete(category);
    }
}
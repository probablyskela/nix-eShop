using AutoMapper;
using Catalog.API.Exceptions.NotFoundExceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Shared.Data.Dtos.CategoryDto;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;

    public CategoryService(IRepositoryManager repositoryManager, ILogger<CategoryService> logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<CategoryDto> categoryDtos, MetaData metaData)> GetCategoriesAsync(
        CategoryParameters productParameters, bool trackChanges)
    {
        var categoryEntities = await _repository.Category.GetCategoriesAsync(productParameters, trackChanges);

        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);

        return (categoryDtos, categoryEntities.MetaData);
    }

    public async Task<CategoryDto> GetCategoryAsync(int categoryId, bool trackChanges)
    {
        var categoryEntity = await GetCategoryIfExistsAsync(categoryId, trackChanges);

        var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

        return categoryDto;
    }

    private async Task<Category> GetCategoryIfExistsAsync(int categoryId, bool trackChanges)
    {
        var categoryEntity = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);

        if (categoryEntity is null)
        {
            throw new CategoryNotFoundException(categoryId);
        }

        return categoryEntity;
    }
}
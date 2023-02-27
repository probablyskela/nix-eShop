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

    public CategoryService(IRepositoryManager repositoryManager, ILogger<CategoryService> logger,
        IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<CategoryDto> categoryDtos, MetaData metaData)> GetCategoriesAsync(
        CategoryParameters categoryParameters, bool trackChanges)
    {
        var categoryEntities = await _repository.Category.GetCategoriesAsync(categoryParameters, trackChanges);

        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);

        _logger.LogInformation(
            $"Returned categories on page: {categoryParameters.PageIndex} with {categoryParameters.PageSize} elements");

        return (categoryDtos, categoryEntities.MetaData);
    }

    public async Task<CategoryDto> GetCategoryAsync(int categoryId, bool trackChanges)
    {
        var categoryEntity = await GetCategoryIfExistsAsync(categoryId, trackChanges);

        var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

        _logger.LogInformation($"Returned category with id: {categoryId}");

        return categoryDto;
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto categoryForCreation)
    {
        var categoryEntity = _mapper.Map<Category>(categoryForCreation);

        await _repository.Category.CreateCategoryAsync(categoryEntity);
        await _repository.SaveAsync();

        var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

        _logger.LogInformation($"Created category with id: {categoryDto.Id}");

        return categoryDto;
    }

    public async Task UpdateCategoryNameAsync(int categoryId, CategoryUpdateNameDto categoryUpdateNameDto)
    {
        var category = await GetCategoryIfExistsAsync(categoryId, trackChanges: true);

        category.Name = categoryUpdateNameDto.Name;

        _logger.LogInformation($"Updated category name with id: {categoryId}");

        await _repository.SaveAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId, bool trackChanges)
    {
        var category = await GetCategoryIfExistsAsync(categoryId, trackChanges);

        _repository.Category.DeleteCategory(category);
        await _repository.SaveAsync();

        _logger.LogInformation($"Deleted category with id: {categoryId}");
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
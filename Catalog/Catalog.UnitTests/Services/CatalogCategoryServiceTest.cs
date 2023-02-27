using AutoMapper;
using Catalog.API.Exceptions;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service;
using Catalog.API.Service.Abstractions;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Data.Dtos.CategoryDto;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.UnitTests.Services;

public class CatalogCategoryServiceTest
{
    private readonly ICategoryService _categoryService;

    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILogger<CategoryService>> _logger;
    private readonly Mock<IMapper> _mapper;

    public CatalogCategoryServiceTest()
    {
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILogger<CategoryService>>();
        _mapper = new Mock<IMapper>();

        _categoryService = new CategoryService(_repository.Object, _logger.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateCategoryAsync_Success()
    {
        // arrange  
        var categoryForCreationDto = new CategoryForCreationDto
        {
            Name = "test"
        };

        var category = new CategoryDto
        {
            Name = "test"
        };

        _repository.Setup(s => s.Category.CreateCategoryAsync(
            It.IsAny<Category>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<CategoryDto>(
            It.IsAny<Category>())).Returns(category);

        // act
        var result = await _categoryService.CreateCategoryAsync(categoryForCreationDto);

        // assert
        result.Should().NotBeNull();
        result.Name.Should().Be(category.Name);
    }

    [Fact]
    public async Task CreateCategoryAsync_Failed()
    {
        // arrange  
        var categoryForCreationDto = new CategoryForCreationDto
        {
            Name = "test"
        };

        var category = new CategoryDto
        {
            Name = null
        };

        _repository.Setup(s => s.Category.CreateCategoryAsync(
            It.IsAny<Category>())).Returns(Task.FromResult(default(object)));

        _mapper.Setup(s => s.Map<CategoryDto>(
            It.IsAny<Category>())).Returns(category);

        // act
        var result = await _categoryService.CreateCategoryAsync(categoryForCreationDto);

        // assert
        result.Name.Should().Be(category.Name);
    }

    [Fact]
    public async Task GetCategoriesAsync_Success()
    {
        // arrange  
        var categoryParameters = new CategoryParameters();

        var categoryDtos = new List<CategoryDto>
        {
            new()
            {
                Name = "test"
            }
        };

        var categoryEntities = new PagedList<Category>(new List<Category>
        {
            new Category
            {
                Name = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Category.GetCategoriesAsync(
            categoryParameters,
            false)).ReturnsAsync(categoryEntities);

        _mapper.Setup(s => s.Map<IEnumerable<CategoryDto>>(
            It.IsAny<IEnumerable<Category>>())).Returns(categoryDtos);

        // act
        var result = await _categoryService.GetCategoriesAsync(categoryParameters, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCategoriesAsync_Failed()
    {
        // arrange  
        var categoryParameters = new CategoryParameters();

        List<CategoryDto>? categoryDtos = null;

        var categoryEntities = new PagedList<Category>(new List<Category>
        {
            new Category
            {
                Name = "test"
            }
        }, 2, 3, 1);

        _repository.Setup(s => s.Category.GetCategoriesAsync(
            categoryParameters,
            false)).ReturnsAsync(categoryEntities);

        _mapper.Setup(s => s.Map<IEnumerable<CategoryDto>>(
            It.IsAny<IEnumerable<Category>>())).Returns(categoryDtos);

        // act
        var result = await _categoryService.GetCategoriesAsync(categoryParameters, false);

        // assert
        result.categoryDtos.Should().BeNull();
    }

    [Fact]
    public async Task GetCategoryAsync_Success()
    {
        // arrange  
        var categoryDto = new CategoryDto
        {
            Name = "test"
        };

        var categoryEntity = new Category
        {
            Name = "test"
        };

        var categoryId = 1;

        _repository.Setup(s => s.Category.GetCategoryAsync(
            It.IsAny<int>(),
            false)).ReturnsAsync(categoryEntity);

        _mapper.Setup(s => s.Map<CategoryDto>(
            It.IsAny<Category>())).Returns(categoryDto);

        // act
        var result = await _categoryService.GetCategoryAsync(categoryId, false);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCategoryAsync_Failed()
    {
        // arrange  
        CategoryDto? categoryDto = null;

        var categoryEntity = new Category
        {
            Name = "test"
        };

        var categoryId = 1;

        _repository.Setup(s => s.Category.GetCategoryAsync(
            It.IsAny<int>(),
            false)).ReturnsAsync(categoryEntity);

        _mapper.Setup(s => s.Map<CategoryDto>(
            It.IsAny<Category>())).Returns(categoryDto);

        // act
        var result = await _categoryService.GetCategoryAsync(categoryId, false);

        // assert
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task UpdateCategoryNameAsync_Success()
    {
        // arrange  
        var categoryId = 1;

        var categoryUpdateNameDto = new CategoryUpdateNameDto
        {
            Name = "asd"
        };
        
        // act
        Func<Task> act = async () =>
        {
            await _categoryService.UpdateCategoryNameAsync(categoryId, categoryUpdateNameDto);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCategoryNameAsync_Failed()
    {
        // arrange  
        var categoryId = 1;

        var categoryUpdateNameDto = new CategoryUpdateNameDto
        {
            Name = "asd"
        };
        
        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _categoryService.UpdateCategoryNameAsync(categoryId, categoryUpdateNameDto);
        };
        // assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }
    
    [Fact]
    public async Task DeleteCategoryAsync_Success()
    {
        // arrange  
        var categoryId = 1;

        _repository.Setup(s => s.Category.DeleteCategory(
            It.IsAny<Category>()));

        // act
        Func<Task> act = async () =>
        {
            await _categoryService.DeleteCategoryAsync(categoryId, false);
        };
        // assert
        act.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteCategoryAsync_Failed()
    {
        // arrange  
        var categoryId = 1;

        _repository.Setup(s => s.Category.DeleteCategory(
            It.IsAny<Category>()));

        _repository.Setup(s => s.SaveAsync())
            .Throws(new InvalidOperationException());

        // act
        Func<Task> act = async () =>
        {
            await _categoryService.DeleteCategoryAsync(categoryId, false);
        };
        // assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
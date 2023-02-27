using Catalog.API.Repository.Abstractions;
using Catalog.API.Repository.Repositories;
using Catalog.API.Repository.Repositories.Abstractions;

namespace Catalog.API.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    private readonly Lazy<IConsumerRepository> _consumerRepository;
    private readonly Lazy<IProductRepository> _itemRepository;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IProductVariantRepository> _productVariantRepository;
    private readonly Lazy<IProductVariantPictureRepository> _productVariantPictureRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;

        _consumerRepository =
            new Lazy<IConsumerRepository>(() => new ConsumerRepository(repositoryContext));
        _itemRepository =
            new Lazy<IProductRepository>(() => new ProductRepository(repositoryContext));
        _categoryRepository =
            new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
        _productVariantRepository =
            new Lazy<IProductVariantRepository>(() => new ProductVariantRepository(repositoryContext));
        _productVariantPictureRepository =
            new Lazy<IProductVariantPictureRepository>(() => new ProductVariantPictureRepository(repositoryContext));
    }

    public IConsumerRepository Consumer => _consumerRepository.Value;
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _itemRepository.Value;
    public IProductVariantRepository ProductVariant => _productVariantRepository.Value;
    public IProductVariantPictureRepository ProductVariantPicture => _productVariantPictureRepository.Value;

    public async Task SaveAsync()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}
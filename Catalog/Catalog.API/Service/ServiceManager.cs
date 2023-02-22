using AutoMapper;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Abstractions;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;

namespace Catalog.API.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IConsumerService> _consumerService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IProductVariantService> _productVariantService;
    private readonly Lazy<IPictureService> _pictureService;

    public ServiceManager(IServiceProvider services)
    {
        _consumerService =
            new Lazy<IConsumerService>(services.GetRequiredService<IConsumerService>);
        _productService =
            new Lazy<IProductService>(services.GetRequiredService<IProductService>);
        _categoryService =
            new Lazy<ICategoryService>(services.GetRequiredService<ICategoryService>);
        _productVariantService =
            new Lazy<IProductVariantService>(services.GetRequiredService<IProductVariantService>());
        _pictureService =
            new Lazy<IPictureService>(services.GetRequiredService<IPictureService>());
    }

    public IConsumerService Consumer => _consumerService.Value;
    public IProductService Product => _productService.Value;
    public ICategoryService Category => _categoryService.Value;
    public IProductVariantService ProductVariant => _productVariantService.Value;
    public IPictureService Picture => _pictureService.Value;
}
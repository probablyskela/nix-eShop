using Catalog.API.Service.Services.Abstractions;

namespace Catalog.API.Service.Abstractions;

public interface IServiceManager
{
    IConsumerService Consumer { get; }
    IProductService Product { get; }
    ICategoryService Category { get; }
    IProductVariantService ProductVariant { get; }
    IPictureService Picture { get; }
}
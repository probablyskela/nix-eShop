using Catalog.API.Repository;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }
    
    public static void ConfigureNpgsqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<RepositoryContext>(opts =>
        {
            opts.UseNpgsql(configuration["ConnectionString"]);
        });
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddScoped<IPictureService, PictureService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductVariantService, ProductVariantService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
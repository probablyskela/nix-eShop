using Catalog.API.Repository;
using Catalog.API.Service.Services;
using Catalog.API.Service.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Filters;

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
        services.AddDbContextFactory<RepositoryContext>(opts => { opts.UseNpgsql(configuration["ConnectionString"]); });
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductVariantService, ProductVariantService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }

    public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "eShop - Catalog HTTP API",
                Version = "v1",
                Description = "The Catalog Service HTTP API"
            });

            var authority = configuration["Authorization:Authority"];
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                        TokenUrl = new Uri($"{authority}/connect/token"),
                        Scopes = new Dictionary<string, string>()
                        {
                            { "mvc", "website" },
                            { "catalog.catalogCategory", "catalog.catalogCategory" },
                            { "catalog.catalogConsumer", "catalog.catalogConsumer" },
                            { "catalog.catalogProduct", "catalog.catalogProduct" }
                        }
                    }
                }
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }
}
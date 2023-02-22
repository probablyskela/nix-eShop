using AutoMapper;
using Shared.Data.Dtos.CategoryDto;
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Entities;

namespace Catalog.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(cpd => cpd.Price,
                opts =>
                {
                    opts.MapFrom(cp =>
                        cp.ProductVariants.Where(cpv => cpv.AvailableStock > 0).DefaultIfEmpty().Min());
                })
            .ForMember(cpd => cpd.AvailableStock,
                opts =>
                {
                    opts.MapFrom(cp => cp.ProductVariants.Sum(s => s.AvailableStock));
                });

        CreateMap<ProductVariant, ProductVariantDto>();
        
        CreateMap<Category, CategoryDto>();
    }
}
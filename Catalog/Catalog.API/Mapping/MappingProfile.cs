using AutoMapper;
using Shared.Data.Dtos.CategoryDto;
using Shared.Data.Dtos.ConsumerDtos;
using Shared.Data.Dtos.PictureDtos;
using Shared.Data.Dtos.ProductDtos;
using Shared.Data.Dtos.ProductVariantDtos;
using Shared.Data.Entities;

namespace Catalog.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(p => p.Price,
                opts =>
                    opts.MapFrom(p => p.ProductVariants
                        .Where(pv => pv.AvailableStock > 0)
                        .Select(pv => pv.Price)
                        .DefaultIfEmpty()
                        .Min()))
            .ForMember(p => p.AvailableStock,
                opts =>
                    opts.MapFrom(p => p.ProductVariants
                        .Sum(pv => pv.AvailableStock)))
            .ForMember(p => p.ConsumerIds,
                opts =>
                    opts.MapFrom(p => p.Consumers));
        CreateMap<ProductForCreationDto, Product>();

        CreateMap<ProductVariant, ProductVariantDto>();
        CreateMap<ProductVariantForCreationDto, ProductVariant>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryForCreationDto, Category>();

        CreateMap<Consumer, ConsumerDto>();
        CreateMap<ConsumerForCreationDto, Consumer>();

        CreateMap<Picture, PictureDto>();
        CreateMap<PictureForCreationDto, Picture>();
    }
}
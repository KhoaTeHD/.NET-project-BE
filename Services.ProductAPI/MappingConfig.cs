using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>()
                        .ForMember(dest => dest.ProductVariations, opt => opt.MapFrom(src => src.ProductVariations));
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<ProductVariation, ProductVariationDto>()
                        .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
                config.CreateMap<ProductVariationDto, ProductVariation>();
                    
            });
            return mappingConfig;
        }
    }
}

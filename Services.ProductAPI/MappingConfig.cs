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
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<ProductVariation, ProductVariationDto>();
                config.CreateMap<ProductVariationDto, ProductVariation>();
            });
            return mappingConfig;
        }
    }
}

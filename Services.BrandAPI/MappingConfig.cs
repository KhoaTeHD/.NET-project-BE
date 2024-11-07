using AutoMapper;
using Services.BrandAPI.Models;
using Services.BrandAPI.Models.Dto;

namespace Services.BrandAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Brand, BrandDto>();
                config.CreateMap<BrandDto, Brand>();
            });
            return mappingConfig;
        }
    }
}

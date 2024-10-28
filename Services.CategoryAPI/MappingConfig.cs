using AutoMapper;
using Services.CategoryAPI.Models;
using Services.CategoryAPI.Models.Dto;

namespace Services.CategoryAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDto>();
                config.CreateMap<CategoryDto, Category>();
            });
            return mappingConfig;
        }
    }
}

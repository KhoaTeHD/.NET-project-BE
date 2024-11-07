using AutoMapper;
using Services.ColorAPI.Models;
using Services.ColorAPI.Models.Dto;

namespace Services.ColorAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Color, ColorDto>();
                config.CreateMap<ColorDto, Color>();
            });
            return mappingConfig;
        }
       
    }
}

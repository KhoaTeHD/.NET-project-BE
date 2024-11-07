using AutoMapper;
using Services.NationAPI.Models;
using Services.NationAPI.Models.Dto;

namespace Services.NationAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Nation, NationDto>();
                config.CreateMap<NationDto, Nation>();
            });
            return mappingConfig;
        }
    }
}

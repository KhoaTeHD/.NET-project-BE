using AutoMapper;
using Services.SizeAPI.Models;
using Services.SizeAPI.Models.Dto;

namespace Services.SizeAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Size, SizeDto>();
                config.CreateMap<SizeDto, Size>();
            });
            return mappingConfig;
        }
    }
}

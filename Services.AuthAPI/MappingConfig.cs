using AutoMapper;
using Services.AuthAPI.Models;
using Services.AuthAPI.Models.Dto;

namespace Services.AuthAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ApplicationUser, UserDto>();
                config.CreateMap<UserDto, ApplicationUser>();
            });
            return mappingConfig;
        }
    }
}

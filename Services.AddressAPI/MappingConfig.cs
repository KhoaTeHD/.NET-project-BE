using AutoMapper;
using Services.AddressAPI.Models;
using Services.AddressAPI.Models.Dto;

namespace Services.AddressAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Address, AddressDto>();
                config.CreateMap<AddressDto, Address>();
            });
            return mappingConfig;
        }
    }
}

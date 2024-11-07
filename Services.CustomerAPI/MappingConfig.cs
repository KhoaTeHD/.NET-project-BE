using Services.CustomerAPI.Models;
using Services.CustomerAPI.Models.Dto;
using AutoMapper;

namespace Services.CustomerAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Customer, CustomerDto>();
                config.CreateMap<CustomerDto, Customer>();
            });
            return mappingConfig;
        }
    }
}

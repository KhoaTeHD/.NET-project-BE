using AutoMapper;
using Service.SupplierAPI.Models.Dto;
using Services.SupplierAPI.Models;

namespace Service.SupplierAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Supplier, SupplierDto>();
                config.CreateMap<SupplierDto, Supplier>();
            });
            return mappingConfig;
        }
    }
}

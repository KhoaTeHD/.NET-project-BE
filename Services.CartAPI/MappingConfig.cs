using AutoMapper;
using Services.CartItemAPI.Models;
using Services.CartItemAPI.Models.Dto;

namespace Services.CartItemAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartItem, CartItemDto>();
                config.CreateMap<CartItemDto, CartItem>();
            });
            return mappingConfig;
        }
    }
}

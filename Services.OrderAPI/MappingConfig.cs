using AutoMapper;
using Services.OrderAPI.Models;
using Services.OrderAPI.Models.Dto;

namespace Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.DetailOrders, opt => opt.MapFrom(src => src.DetailOrders));
                config.CreateMap<OrderDto, Order>();
                config.CreateMap<DetailOrder, DetailOrderDto>();
                config.CreateMap<DetailOrderDto, DetailOrder>();
            });
            return mappingConfig;
        }
    }
}

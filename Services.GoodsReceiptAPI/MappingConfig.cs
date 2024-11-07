using AutoMapper;
using Services.GoodsReceiptAPI.Models;
using Services.GoodsReceiptAPI.Models.Dto;

namespace Services.GoodsReceiptAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<GoodsReceipt, GoodsReceiptDto>();
                config.CreateMap<GoodsReceiptDto, GoodsReceipt>();
                config.CreateMap<DetailGoodsReceipt, DetailGoodsReceiptDto>();
                config.CreateMap<DetailGoodsReceiptDto, DetailGoodsReceipt>();
            });
            return mappingConfig;
        }
    }
}

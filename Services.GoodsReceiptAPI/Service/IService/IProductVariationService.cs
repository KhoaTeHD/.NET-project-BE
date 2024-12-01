using Services.GoodsReceiptAPI.Models.Dto;

namespace Services.GoodsReceiptAPI.Service.IService
{
    public interface IProductVariationService
    {
        Task<IEnumerable<ProductVariationDto>> GetProductVariations();
    }
}

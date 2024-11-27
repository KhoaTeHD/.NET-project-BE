using Services.OrderAPI.Models.Dto;

namespace Services.OrderAPI.Service.IService
{
    public interface IProductVariationService
    {
        Task<IEnumerable<ProductVariationDto>> GetProductVariations();
    }
}

using Services.CartItemAPI.Models.Dto;

namespace Services.CartItemAPI.Service.IService
{
    public interface IProductVariationService
    {
        Task<IEnumerable<ProductVariationDto>> GetProductVariations();
    }
}

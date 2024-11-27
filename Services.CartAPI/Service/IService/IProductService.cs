using Services.CartItemAPI.Models.Dto;

namespace Services.CartItemAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}

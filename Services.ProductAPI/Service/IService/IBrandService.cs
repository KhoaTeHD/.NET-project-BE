using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Service.IService
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetBrands();
    }
}

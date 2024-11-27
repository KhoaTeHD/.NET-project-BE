using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Service.IService
{
    public interface ISizeService
    {
        Task<IEnumerable<SizeDto>> GetSizes();
    }
}

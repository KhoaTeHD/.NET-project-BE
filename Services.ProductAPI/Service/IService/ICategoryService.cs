using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Service.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategorys();
    }
}

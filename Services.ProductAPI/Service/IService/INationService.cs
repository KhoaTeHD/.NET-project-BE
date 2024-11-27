using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Service.IService
{
    public interface INationService
    {
        Task<IEnumerable<NationDto>> GetNations();
    }
}

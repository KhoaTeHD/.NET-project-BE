using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Service.IService
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetSuppliers();
    }
}

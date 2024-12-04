using Services.GoodsReceiptAPI.Models.Dto;

namespace Services.GoodsReceiptAPI.Service.IService
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetSuppliers();
    }
}

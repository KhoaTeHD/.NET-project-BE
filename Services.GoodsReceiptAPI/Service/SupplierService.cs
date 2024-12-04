using Newtonsoft.Json;
using Services.GoodsReceiptAPI.Models.Dto;
using Services.GoodsReceiptAPI.Service.IService;

namespace Services.GoodsReceiptAPI.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly IHttpClientFactory _clientFactory;
        public SupplierService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<SupplierDto>> GetSuppliers()
        {
            var client = _clientFactory.CreateClient("Supplier");
            var response = await client.GetAsync("https://localhost:7777/api/Supplier");

            if (response.IsSuccessStatusCode)
            {
                var apiContet = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiContet))
                {
                    var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
                    if (resp != null && resp.IsSuccess)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<SupplierDto>>(Convert.ToString(resp.Result));
                    }
                }
            }
            return new List<SupplierDto>();
        }

    }
}

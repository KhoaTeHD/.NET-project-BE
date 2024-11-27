using Newtonsoft.Json;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Service.IService;

namespace Services.ProductAPI.Service
{
    public class BrandService : IBrandService
    {
        private readonly IHttpClientFactory _clientFactory;
        public BrandService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var client = _clientFactory.CreateClient("Brand");
            var response = await client.GetAsync("https://localhost:7777/api/Brand");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseProductDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<BrandDto>>(Convert.ToString(resp.Result));
            }
            return new List<BrandDto>();
        }
    }
}

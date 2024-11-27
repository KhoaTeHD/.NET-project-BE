using Newtonsoft.Json;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Service.IService;

namespace Services.ProductAPI.Service
{
    public class SizeService : ISizeService
    {
        private readonly IHttpClientFactory _clientFactory;
        public SizeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<SizeDto>> GetSizes()
        {
            var client = _clientFactory.CreateClient("Size");
            var response = await client.GetAsync("https://localhost:7777/api/Size");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseProductDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<SizeDto>>(Convert.ToString(resp.Result));
            }
            return new List<SizeDto>();
        }
    }
}

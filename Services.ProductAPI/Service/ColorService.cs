using Newtonsoft.Json;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Service.IService;

namespace Services.ProductAPI.Service
{
    public class ColorService : IColorService
    {
        private readonly IHttpClientFactory _clientFactory;
        public ColorService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<ColorDto>> GetColors()
        {
            var client = _clientFactory.CreateClient("Color");
            var response = await client.GetAsync("https://localhost:7777/api/Color");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseProductDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ColorDto>>(Convert.ToString(resp.Result));
            }
            return new List<ColorDto>();
        }
    }
}

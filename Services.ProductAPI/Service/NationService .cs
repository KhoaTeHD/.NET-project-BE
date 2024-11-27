using Newtonsoft.Json;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Service.IService;

namespace Services.ProductAPI.Service
{
    public class NationService : INationService
    {
        private readonly IHttpClientFactory _clientFactory;
        public NationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<NationDto>> GetNations()
        {
            var client = _clientFactory.CreateClient("Nation");
            var response = await client.GetAsync("https://localhost:7777/api/Nation");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseProductDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<NationDto>>(Convert.ToString(resp.Result));
            }
            return new List<NationDto>();
        }
    }
}

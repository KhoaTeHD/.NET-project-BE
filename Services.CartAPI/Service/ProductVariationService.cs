using Newtonsoft.Json;
using Services.CartItemAPI.Models.Dto;
using Services.CartItemAPI.Service.IService;

namespace Services.CartItemAPI.Service
{
    public class ProductVariationService : IProductVariationService
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductVariationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProductVariationDto>> GetProductVariations()
        {
            var client = _clientFactory.CreateClient("Product");
            var response = await client.GetAsync("https://localhost:7777/api/ProductVariation");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseCartItemDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductVariationDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductVariationDto>();
        }
    }
}

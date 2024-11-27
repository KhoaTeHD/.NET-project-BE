using Newtonsoft.Json;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Service.IService;

namespace Services.ProductAPI.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategorys()
        {
            var client = _clientFactory.CreateClient("Category");
            var response = await client.GetAsync("https://localhost:7777/api/Category");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseProductDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(Convert.ToString(resp.Result));
            }
            return new List<CategoryDto>();
        }
    }
}

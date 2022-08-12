using System.Text.Json;
using VShopWeb.Models;

namespace VShopWeb.Services.Contracts
{
    public class CategoryService : ICategoryService
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        private const string apiEndPoint = "/api/categories/";
        private ProductViewModel productVM;
        private IEnumerable<ProductViewModel> productsVM;

        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var client = _clientFactory.CreateClient("ProductApi");

            IEnumerable<CategoryViewModel> categories;

            var response = await client.GetAsync(apiEndPoint);
            
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            
            return categories;
        }
    }
}

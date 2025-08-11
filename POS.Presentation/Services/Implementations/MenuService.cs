using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _httpClient;
        public MenuService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<List<MenuModel>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/Menu");
            response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code
            var result = await response.Content.ReadFromJsonAsync<List<MenuModel>>();
            return result ?? new List<MenuModel>();
        }

        internal object GetMenuItems()
        {
            throw new NotImplementedException();
        }
    }
}

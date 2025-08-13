using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using System.Collections.Generic;

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
            List<MenuModel> list = new List<MenuModel>();
            try
            {
                var response = await _httpClient.GetAsync("api/Menu");
                response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code
                list = await response.Content.ReadFromJsonAsync<List<MenuModel>>();

            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

        internal object GetMenuItems()
        {
            throw new NotImplementedException();
        }
    }
}

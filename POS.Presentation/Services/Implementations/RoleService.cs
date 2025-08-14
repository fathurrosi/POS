using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Services.Implementations
{
    public class RoleService: IRoleService
    {
        private readonly HttpClient _httpClient;
        public RoleService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }


        public async Task<List<RoleModel>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/Role");
            response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code
            var data = await response.Content.ReadFromJsonAsync<List<RoleModel>>();
            return data;
        }

        // POST example
        public async Task<RoleModel> CreateDataAsync(RoleModel newData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Role", newData);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<RoleModel>();
            return createdData;
        }

        public async Task<RoleModel> GetByUsername(string username)
        {
            var response = await _httpClient.GetAsync($"api/Role/{username}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RoleModel>();
        }

    }
}

using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Services.Implementations
{
    public class UserService: IUserService
    {

        private readonly HttpClient _httpClient;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<List<UserModel>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/User");
            response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code
            var data = await response.Content.ReadFromJsonAsync<List<UserModel>>();
            return data;
        }

        // POST example
        public async Task<UserModel> CreateDataAsync(UserModel newData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User", newData);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<UserModel>();
            return createdData;
        }
    }
}

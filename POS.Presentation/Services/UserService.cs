using POS.Presentation.Models;
using System.Net.Http;

namespace POS.Presentation.Services
{
    public class UserService
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient= httpClient;
        }

        //public async Task<UserModel> GetDataAsync()
        //{
        //    var client = _httpClient.CreateClient("MyApi");
        //    var response = await client.GetAsync("data");
        //    response.EnsureSuccessStatusCode();
        //    var data = await response.Content.ReadAsAsync<MyModel>();
        //    return data;
        //}

        // GET example
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

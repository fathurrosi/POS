using Microsoft.Build.Framework;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
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
        public async Task<List<Menu>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/Menu");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Menu>>();

        }

        public async Task<PagingResult<Menu>> GetPagingAsync(int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/Menu/Paging/{pageIndex}/{pageSize}");
            response.EnsureSuccessStatusCode();
            PagingResult<Menu> result = await response.Content.ReadFromJsonAsync<PagingResult<Menu>>();

            return result;
        }

    }
}

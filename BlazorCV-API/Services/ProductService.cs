using BlazorCV_API.Models;
using System.Net.Http.Json;

namespace BlazorCV_API.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddProject(Project project)
        {
            var response = await _httpClient.PostAsJsonAsync("api/projects", project);
            return response.IsSuccessStatusCode;
        }
    }
}

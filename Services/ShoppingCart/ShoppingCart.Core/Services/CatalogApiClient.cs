using Microsoft.Extensions.Options;
using ShoppingCart.Core.Entities;
using ShoppingCart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Services
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly IOptions<AppSettings> _settings;

        public CatalogApiClient(HttpClient apiClient,
            IOptions<AppSettings> settings)
        {
            _apiClient = apiClient;
            _settings = settings;
        }

        public async Task<Product?> GetProduct(int id, int storeId)
        {
            var url = $"{_settings.Value.CatalogApiUrl}/product/{id}/{storeId}";
            var response = await _apiClient.GetAsync(url);
            if (response.IsSuccessStatusCode == false)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(responseString) ?
                null :
                JsonSerializer.Deserialize<Product>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}

using BlazorApp2.Shared;
using System.Net.Http.Json;

namespace BlazorApp2.Service
{
    public class ProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await httpClient.GetFromJsonAsync<List<Product>>("sample-data/products.json");
            return products ?? new List<Product>();
        }
    }
}

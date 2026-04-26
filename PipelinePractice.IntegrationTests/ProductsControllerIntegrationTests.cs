using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using PipelinePractice.Models;
using Xunit;

namespace PipelinePractice.IntegrationTests
{
    public class ProductsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOkAndList()
        {
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
            Assert.NotNull(products);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAndProduct()
        {
            var request = new CreateProductRequest { Name = "Integration Product", Price = 99 };
            var response = await _client.PostAsJsonAsync("/api/products", request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var product = await response.Content.ReadFromJsonAsync<ProductResponse>();
            Assert.NotNull(product);
            Assert.Equal(request.Name, product.Name);
            Assert.Equal(request.Price, product.Price);
        }
    }
}

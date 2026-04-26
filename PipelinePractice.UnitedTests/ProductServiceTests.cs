using PipelinePractice.Models;
using PipelinePractice.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PipelinePractice.UnitedTests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetAll_ReturnsOnlyActiveProducts()
        {
            // Arrange
            var service = new ProductService();
            service.Create(new CreateProductRequest { Name = "Active Product", Price = 10 });
            var inactiveProduct = new Product { Id = Guid.NewGuid(), Name = "Inactive", Price = 5, Active = false };
            // Adicionando produto inativo diretamente via reflexão
            var productsField = typeof(ProductService).GetField("_products", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var productsList = (List<Product>)productsField.GetValue(service);
            productsList.Add(inactiveProduct);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal("Active Product", result[0].Name);
        }

        [Fact]
        public void Create_AddsProductAndReturnsResponse()
        {
            // Arrange
            var service = new ProductService();
            var request = new CreateProductRequest { Name = "Test Product", Price = 20 };

            // Act
            var response = service.Create(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Price, response.Price);
            Assert.NotEqual(Guid.Empty, response.Id);
        }
    }
}

using PipelinePractice.Models;

namespace PipelinePractice.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();

        public List<ProductResponse> GetAll()
        {
            return _products
                .Where(p => p.Active)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();
        }

        public ProductResponse Create(CreateProductRequest request)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Active = true
            };

            _products.Add(product);

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}

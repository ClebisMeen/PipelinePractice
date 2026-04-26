using System.Collections.Generic;
using PipelinePractice.Models;

namespace PipelinePractice.Services
{
    public interface IProductService
    {
        List<ProductResponse> GetAll();
        ProductResponse Create(CreateProductRequest request);
    }
}

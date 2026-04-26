using Microsoft.AspNetCore.Mvc;
using PipelinePractice.Models;
using PipelinePractice.Services;

namespace PipelinePractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductResponse>> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpPost]
        public ActionResult<ProductResponse> Create([FromBody] CreateProductRequest request)
        {
            var product = _productService.Create(request);
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }
    }
}

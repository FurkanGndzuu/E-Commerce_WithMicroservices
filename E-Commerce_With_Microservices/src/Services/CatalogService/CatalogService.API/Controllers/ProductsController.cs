using CatalogService.API.Abstractions;
using CatalogService.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product) => Ok(await _productService.CreateProduct(product));
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product) => Ok(await _productService.UpdateProduct(product));
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string Id) => Ok(await _productService.DeleteProduct(Id));
        [HttpPost("add-photo")]
        public async Task<IActionResult> AddProductPhoto([FromBody] IFormFileCollection files, [FromQuery] string ProductId) => Ok(await _productService.AddProductPhoto(files, ProductId));
        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] string Id) => Ok(await _productService.GetProductById(Id));
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await _productService.GetAllProducts());
    }
}

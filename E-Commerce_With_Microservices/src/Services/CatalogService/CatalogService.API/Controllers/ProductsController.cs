using CatalogService.API.Abstractions;
using CatalogService.API.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "Write")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product) => Ok(await _productService.CreateProduct(product));
        [Authorize(Policy = "Write")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product) => Ok(await _productService.UpdateProduct(product));
        [Authorize(Policy = "Write")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string Id) => Ok(await _productService.DeleteProduct(Id));
        [Authorize(Policy = "Write")]
        [HttpPost("add-photo")]
        public async Task<IActionResult> AddProductPhoto([FromBody] IFormFileCollection files, [FromQuery] string ProductId) => Ok(await _productService.AddProductPhoto(files, ProductId));
        [Authorize(Policy = "Read")]
        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] string Id) => Ok(await _productService.GetProductById(Id));
        [Authorize(Policy = "Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await _productService.GetAllProducts());
    }
}

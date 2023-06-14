using CatalogService.API.DTOs;
using SharedService.Responses;

namespace CatalogService.API.Abstractions
{
    public interface IProductService
    {
        public Task<Response<NoContent>> CreateProduct(CreateProductDto product);
        public Task<Response<NoContent>> UpdateProduct(UpdateProductDto updateProduct);
        public Task<Response<NoContent>> DeleteProduct(string Id);
        public Task<Response<List<ProductDTO>>> GetAllProducts();
        public Task<Response<ProductDTO>> GetProductById(string id);
        public Task<Response<NoContent>> AddProductPhoto(IFormFileCollection files , string ProductId);
    }
}

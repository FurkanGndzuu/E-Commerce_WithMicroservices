using CatalogService.API.DTOs;
using SharedService.Responses;

namespace CatalogService.API.Abstractions
{
    public interface ICategoryService
    {
        public Task<Response<NoContent>> AddCategory(string categoryName);
        public Task<Response<NoContent>> RemoveCategoryById(string Id);
        public Task<Response<NoContent>> UpdateCategory(CategoryDTO category);
        public Task<Response<List<CategoryDTO>>> GetAllCategories();
        public Task<Response<CategoryDTO>> GetCategoryById(string Id);
    }
}

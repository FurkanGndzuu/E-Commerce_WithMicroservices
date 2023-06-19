using CatalogService.API.Abstractions;
using CatalogService.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Policy = "Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() => Ok(await _categoryService.GetAllCategories());
        [Authorize(Policy = "Read")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(string Id) => Ok(await _categoryService.GetCategoryById(Id));
        [Authorize(Policy = "Write")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromQuery] string Name) => Ok(await _categoryService.AddCategory(Name));
        [Authorize(Policy = "Write")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category) => Ok(await _categoryService.UpdateCategory(category));
        [Authorize(Policy = "Write")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory(string Id) => Ok(await _categoryService.RemoveCategoryById(Id));

    }
}

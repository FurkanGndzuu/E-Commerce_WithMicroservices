using CatalogService.API.Abstractions.Repositories.CategoryRepositories;
using CatalogService.API.Models.Contexts;
using CatalogService.API.Models.Entities;

namespace CatalogService.API.Services.Repositories.CategoryRepositories
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(CatalogDbContext _context) : base(_context)
        {
        }
    }
}

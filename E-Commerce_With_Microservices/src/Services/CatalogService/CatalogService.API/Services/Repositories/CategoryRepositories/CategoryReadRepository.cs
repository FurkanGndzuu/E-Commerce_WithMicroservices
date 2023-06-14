using CatalogService.API.Abstractions.Repositories.CatalogRepositories;
using CatalogService.API.Models.Contexts;
using CatalogService.API.Models.Entities;

namespace CatalogService.API.Services.Repositories.CategoryRepositories
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(CatalogDbContext _context) : base(_context)
        {
        }
    }
}

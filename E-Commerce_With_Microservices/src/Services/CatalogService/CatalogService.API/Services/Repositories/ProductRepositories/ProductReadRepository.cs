using CatalogService.API.Abstractions.Repositories;
using CatalogService.API.Abstractions.Repositories.ProductRepositories;
using CatalogService.API.Models.Contexts;
using CatalogService.API.Models.Entities;

namespace CatalogService.API.Services.Repositories.ProductRepositories
{
    public class ProductReadRepository : ReadRepository<Product> , IProductReadRepository
    {
        public ProductReadRepository(CatalogDbContext _context) : base(_context)
        {
        }
    }
}

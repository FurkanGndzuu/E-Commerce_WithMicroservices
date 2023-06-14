using CatalogService.API.Abstractions.Repositories.ProductRepositories;
using CatalogService.API.Models.Contexts;
using CatalogService.API.Models.Entities;

namespace CatalogService.API.Services.Repositories.ProductRepositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(CatalogDbContext _context) : base(_context)
        {
        }
    }
}

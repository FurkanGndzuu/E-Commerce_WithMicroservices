using CatalogService.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.Models.Contexts
{
    public class CatalogDbContext : DbContext
    {
        protected CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

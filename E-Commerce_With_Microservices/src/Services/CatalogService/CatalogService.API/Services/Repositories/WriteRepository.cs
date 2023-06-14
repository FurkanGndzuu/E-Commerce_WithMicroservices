using CatalogService.API.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CatalogService.API.Models.Contexts;

namespace CatalogService.API.Services.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        readonly CatalogDbContext context;

        public WriteRepository(CatalogDbContext _context)
        {
            context = _context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> en = await Table.AddAsync(model);
            return en.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true;
        }

        public bool Remove(T model)
        {
            Table.Remove(model);
            return true;
        }

        public bool RemoveRange(List<T> model)
        {
            Table.RemoveRange(model);
            return true;
        }

        public async Task<int> SaveAsync()
         => await context.SaveChangesAsync();

        public bool Update(T model)
        {
            Table.Update(model);
            return true;
        }
    }
}

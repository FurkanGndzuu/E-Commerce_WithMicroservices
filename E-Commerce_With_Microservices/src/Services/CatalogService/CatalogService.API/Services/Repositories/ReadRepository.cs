using CatalogService.API.Abstractions.Repositories;
using CatalogService.API.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogService.API.Services.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        readonly CatalogDbContext context;
        public ReadRepository(CatalogDbContext _context)
        {
            context = _context;
        }
        public DbSet<T> Table => context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {

            var querry = Table.AsQueryable();
            if (!tracking)
            {
                querry = querry.AsNoTracking();
            }
            return querry;

        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var querry = Table.AsQueryable();
            if (!tracking)
            {
                querry = querry.AsNoTracking();
            }
            return await querry.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var querry = Table.Where(method);
            if (!tracking)
            {
                querry = querry.AsNoTracking();
            }
            return querry.AsQueryable();
        }
    }
}

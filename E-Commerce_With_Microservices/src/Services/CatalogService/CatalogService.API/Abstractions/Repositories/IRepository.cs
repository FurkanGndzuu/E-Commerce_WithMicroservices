using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }

    }
}

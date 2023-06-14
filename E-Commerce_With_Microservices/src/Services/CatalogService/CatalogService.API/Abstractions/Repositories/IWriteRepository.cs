using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.Abstractions.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> model);
        bool Remove(T model);
        bool RemoveRange(List<T> model);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}

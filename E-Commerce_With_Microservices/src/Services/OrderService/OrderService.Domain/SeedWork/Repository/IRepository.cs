using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork.Repository
{
    public interface IRepository<T> where T : Entity
    {
        DbSet<T> Table { get; }
    }
}

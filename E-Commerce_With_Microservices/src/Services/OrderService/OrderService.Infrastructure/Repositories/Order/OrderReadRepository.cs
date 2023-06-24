using OrderService.Application.Repositories.Order;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories.Order
{
    public class OrderReadRepository : ReadRepository<Domain.AggregateModels.Order>, IOrderReadRepository
    {
        public OrderReadRepository(OrderDbContext context) : base(context)
        {
        }
    }
}

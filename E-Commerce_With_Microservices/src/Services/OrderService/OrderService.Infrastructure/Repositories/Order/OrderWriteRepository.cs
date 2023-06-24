using OrderService.Application.Repositories.Order;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<Domain.AggregateModels.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(OrderDbContext context) : base(context)
        {
        }
    }
}

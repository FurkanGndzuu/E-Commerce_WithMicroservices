using OrderService.Application.Repositories.OrderItem;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories.OrderItem
{
    public class OrderItemWriteRepository : WriteRepository<Domain.AggregateModels.OrderItem>, IOrderItemWriteRepository
    {
        public OrderItemWriteRepository(OrderDbContext context) : base(context)
        {
        }
    }
}

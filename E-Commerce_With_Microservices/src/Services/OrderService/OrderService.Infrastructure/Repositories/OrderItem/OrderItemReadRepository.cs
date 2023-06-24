using OrderService.Application.Repositories.OrderItem;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories.OrderItem
{
    public class OrderItemReadRepository : ReadRepository<Domain.AggregateModels.OrderItem> , IOrderItemReadRepository
    {
        public OrderItemReadRepository(OrderDbContext context) : base(context)
        {
        }
    }
}

using OrderService.Domain.AggregateModels;
using OrderService.Domain.SeedWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Repositories.OrderItem
{
    public interface IOrderItemReadRepository : IReadRepository<Domain.AggregateModels.OrderItem>
    {
    }
}

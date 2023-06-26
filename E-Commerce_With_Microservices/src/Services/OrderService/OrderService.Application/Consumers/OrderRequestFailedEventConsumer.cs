using MassTransit;
using OrderService.Application.Repositories.Order;
using SharedService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class OrderRequestFailedEventConsumer : IConsumer<IOrderRequestFailedEvent>
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderRequestFailedEventConsumer(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task Consume(ConsumeContext<IOrderRequestFailedEvent> context)
        {
          var order = await  _orderReadRepository.GetSingleAsync(x => x.Id == context.Message.OrderId);

            if (order is not null)
            {
                order.Status = OrderStatus.Failed;
                await _orderWriteRepository.SaveAsync();
            }
            else throw new Exception();
        }
    }
}

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
    public class OrderCompletedEventConsumer : IConsumer<IOrderCompletedRequestEvent>
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderCompletedEventConsumer(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task Consume(ConsumeContext<IOrderCompletedRequestEvent> context)
        {
            var order = await _orderReadRepository.GetSingleAsync(x => x.Id == context.Message.OrderId);

            if (order is not null)
            {
                order.Status = OrderStatus.Completed;
                await _orderWriteRepository.SaveAsync();
            }
            else throw new Exception();
        }
    }
}

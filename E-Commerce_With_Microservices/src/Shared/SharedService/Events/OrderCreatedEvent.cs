using SharedService.Abstractions;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }

        public OrderCreatedEvent(Guid correlation)
        {
            CorrelationId = correlation;
        }
    }
}

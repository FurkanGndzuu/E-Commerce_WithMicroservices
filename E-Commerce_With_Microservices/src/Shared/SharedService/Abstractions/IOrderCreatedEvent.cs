using MassTransit;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IOrderCreatedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }


    }
}

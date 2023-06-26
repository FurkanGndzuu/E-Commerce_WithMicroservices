using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IPaymentFailedEvent
    {
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; }
    }
}

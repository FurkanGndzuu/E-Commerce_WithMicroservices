using SharedService.Abstractions;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class PaymentFailedEvent : IPaymentFailedEvent
    {
        public PaymentFailedEvent(Guid correlationId)
        {
                CorrelationId = correlationId;
        }
        public Guid CorrelationId {get;}

        public List<OrderItemMessage> OrderItems { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; }
    }
}

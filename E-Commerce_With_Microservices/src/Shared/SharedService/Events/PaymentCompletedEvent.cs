using SharedService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class PaymentCompletedEvent : IPaymentCompletedEvent
    {
        public Guid CorrelationId { get; }
        public PaymentCompletedEvent(Guid Correlation)
        {
            CorrelationId = Correlation;
        }
    }
}

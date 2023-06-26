using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IPaymentCompletedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; }
        
    }
}

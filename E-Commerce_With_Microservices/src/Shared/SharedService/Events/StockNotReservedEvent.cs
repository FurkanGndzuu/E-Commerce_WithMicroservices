using SharedService.Abstractions;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class StockNotReservedEvent : IStockNotReservedEvent
    {
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public string FailMessage { get; set; }

        public StockNotReservedEvent(Guid correlation)
        {
            CorrelationId = correlation;
        }
    }
}

using SharedService.Abstractions;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class StockReservedRequestPaymentEvent : IStockReservedRequestPaymentEvent
    {
        public StockReservedRequestPaymentEvent(Guid correlationId)
        {
               CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }

        public decimal TotalPrice {get; set;}
        public string walletId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public string UserId { get; set; }
    }
}

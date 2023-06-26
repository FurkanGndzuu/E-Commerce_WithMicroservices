using MassTransit;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IStockReservedRequestPaymentEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; }
        public decimal TotalPrice { get; set; }
        public string walletId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public string UserId { get; set; }
    }
}

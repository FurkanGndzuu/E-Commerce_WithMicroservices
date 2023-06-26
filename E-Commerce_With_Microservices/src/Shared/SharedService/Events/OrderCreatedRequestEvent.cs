using SharedService.Abstractions;
using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class OrderCreatedRequestEvent : IOrderCreatedRequestEvent
    {
        public int OrderId { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
        public string WalletId { get; set; }
        public decimal TotalPrice { get; set; }


    }
}

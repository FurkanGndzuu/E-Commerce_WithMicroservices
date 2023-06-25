using SharedService.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IOrderCreatedRequestEvent
    {
        public int OrderId { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public string WalletId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Messages
{
    public class OrderItemMessage
    {
        public string ProductId { get; set; }
        public int Count { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Messages
{
    public class OrderItemMessage
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

    }
}

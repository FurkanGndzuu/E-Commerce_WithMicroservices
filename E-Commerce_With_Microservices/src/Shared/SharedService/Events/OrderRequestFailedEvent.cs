using SharedService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class OrderRequestFailedEvent : IOrderRequestFailedEvent
    {
        public int OrderId { get; set; }
        public string FailedMessage { get; set; }
        public string UserId { get; set; }
    }
}

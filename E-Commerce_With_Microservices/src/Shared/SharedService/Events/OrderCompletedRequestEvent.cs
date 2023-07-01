using SharedService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Events
{
    public class OrderCompletedRequestEvent : IOrderCompletedRequestEvent
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IOrderRequestFailedEvent
    {
        public int OrderId { get; set; }
        public string FailedMessage { get; set; }
    }
}

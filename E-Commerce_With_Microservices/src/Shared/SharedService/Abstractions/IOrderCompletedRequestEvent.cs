using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Abstractions
{
    public interface IOrderCompletedRequestEvent
    {
        public int OrderId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedService.Settings
{
    public static class RabbitmqSettings
    {
        public static string OrderSaga = "Order-Saga-Queue";
        public static string OrderCreatedSaga = "Order-Created-Saga-Queue";
        public static string StockReservedSaga = "Stock-Reserved-Payment-Queue";
    }
}

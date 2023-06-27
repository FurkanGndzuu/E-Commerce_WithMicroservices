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
        public static string PaymentStockReservedEvent = "Payment-Saga-Queue";
        public static string OrderSagaFailed = "Order-Saga-Failed-Queue";
        public static string OrderSagaCompleted = "Order-Saga-Completed-Queue";
        public static string PaymentFailedSagaQueue = "Payment-Failed-Saga-Queue";
    }
}

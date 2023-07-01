using MassTransit;
using SharedService.Abstractions;
using SharedService.Events;
using SharedService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrchestration_WorkerService.Models
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }
        public Event<IStockReservedEvent> StockReservedEvent { get; set; }

        public Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; set; }
        public Event<IStockNotReservedEvent> StockNotReservedEvent { get; set; }
        public Event<IPaymentFailedEvent> PaymentFailedEvent { get; set; }

        public State StockReserved { get; private set; }
        public State OrderCreated { get; private set; }
        public State PaymentCompleted { get; set; }
        public State StockNotReserved { get; set; }
        public State PaymentFailed { get; set; }
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreatedRequestEvent , y => y.CorrelateBy<int>(x => x.OrderId ,z => z.Message.OrderId ).SelectId(context => Guid.NewGuid()));

            Event(() => StockReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => StockNotReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentCompletedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentFailedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));


            Initially(When(OrderCreatedRequestEvent).Then(context =>
            {
                context.Saga.BuyerId = context.Message.BuyerId;

                context.Saga.OrderId = context.Message.OrderId;
                context.Saga.CreatedDate = DateTime.Now;
                context.Saga.WalletId = context.Message.WalletId;
                context.Saga.TotalPrice = context.Message.TotalPrice;

            }
            ).TransitionTo(OrderCreated).Send(new Uri($"queue:{RabbitmqSettings.OrderCreatedSaga}"),context => new OrderCreatedEvent(context.Instance.CorrelationId) { OrderItems = context.Message.OrderItems}));


            During(OrderCreated, When(StockReservedEvent).TransitionTo(StockReserved).Send(new Uri($"queue:{RabbitmqSettings.StockReservedSaga}"), context =>new StockReservedRequestPaymentEvent(context.Instance.CorrelationId)
            {
                TotalPrice = context.Saga.TotalPrice,
                walletId = context.Saga.WalletId,
                OrderItems = context.Message.OrderItems,
                UserId = context.Saga.BuyerId
            }),When(StockNotReservedEvent).TransitionTo(StockNotReserved).Publish(context => new OrderRequestFailedEvent()
            {
                OrderId = context.Saga.OrderId,
                FailedMessage = "There Are Not Enough Stock",
                UserId = context.Saga.BuyerId
            }));


            During(StockReserved, When(PaymentCompletedEvent).TransitionTo(PaymentCompleted).Publish(context => new OrderCompletedRequestEvent()
            {
                OrderId = context.Saga.OrderId,
                UserId = context.Saga.BuyerId
            }),When(PaymentFailedEvent).TransitionTo(PaymentFailed).Publish(context => new OrderRequestFailedEvent()
            {
                OrderId = context.Saga.OrderId,
                FailedMessage = "There are not enough money in your wallet",
                UserId = context.Saga.BuyerId
            }));

            SetCompletedWhenFinalized();
        }
    }
}

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
        public State OrderCreated { get; set; }
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreatedRequestEvent , y => y.CorrelateBy<int>(x => x.OrderId ,z => z.Message.OrderId ).SelectId(context => Guid.NewGuid()));

            Initially(When(OrderCreatedRequestEvent).Then(context =>
            {
                context.Saga.BuyerId = context.Message.BuyerId;

                context.Saga.OrderId = context.Message.OrderId;
                context.Saga.CreatedDate = DateTime.Now;
                context.Saga.WalletId = context.Message.WalletId;

            }
            ).TransitionTo(OrderCreated).Send(new Uri($"queue:{RabbitmqSettings.OrderCreatedSaga}"),context => new OrderCreatedEvent(context.Instance.CorrelationId) { OrderItems = context.Message.OrderItems}));
        }
    }
}

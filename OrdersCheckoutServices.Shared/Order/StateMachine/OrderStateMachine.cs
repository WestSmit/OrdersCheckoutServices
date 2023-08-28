using MassTransit;
using OrdersCheckoutServices.Shared.Order.Contracts;

namespace OrdersCheckoutServices.Shared.Order.StateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State Created { get; private set; }
        public State Notified { get; private set; }
        public State Canceled { get; private set; }
        public Event<OrderCreated> OrderCreated { get; private set; }
        public Event<NotificationRequested> NotificationRequested { get; private set; }
        public Event<NotificationSent> NotificationSent { get; private set; }
        public Event<NotificationSendingFailed> NotificationSendingFailed { get; private set; }

        public Guid CorrelationId { get; set; }

        public OrderStateMachine()
        {
            Event(() => OrderCreated, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => NotificationRequested, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => NotificationSent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => NotificationSendingFailed, x => x.CorrelateById(m => m.Message.OrderId));

            InstanceState(x => x.CurrentState);

            Initially(
                When(OrderCreated)
                    .Then(x =>
                    {
                        x.Instance.OrderId = x.Data.OrderId;
                        x.Instance.CustomerId = x.Data.CustomerId;
                        x.Instance.CreatedAt = x.Data.CreatedAt;
                        x.Instance.UpdatedAt = DateTime.UtcNow;
                    })
                    .TransitionTo(Created)
                    .Publish(x => new NotificationRequested() { OrderId = x.Data.OrderId, CustomerId = x.Data.CustomerId }));

            During(Created, Ignore(OrderCreated));
            During(Notified, Ignore(OrderCreated), Ignore(NotificationSent), Ignore(NotificationSendingFailed));
            During(Canceled, Ignore(OrderCreated), Ignore(NotificationSent), Ignore(NotificationSendingFailed));
            DuringAny(Ignore(NotificationRequested));

            During(Created,
                When(NotificationSent)
                    .Then(x =>
                    {
                        x.Instance.UpdatedAt = DateTime.UtcNow;
                        x.Instance.NotificationSentAt = DateTime.UtcNow;

                    })
                    .TransitionTo(Notified),
                When(NotificationSendingFailed)
                    .Then(x =>
                    {
                        x.Instance.UpdatedAt = DateTime.UtcNow;
                        x.Instance.Reason = "Notification service has failed";
                    })
                    .TransitionTo(Canceled));
        }
    }
}

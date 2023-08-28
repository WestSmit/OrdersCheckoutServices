using MassTransit;

namespace OrdersCheckoutServices.Shared
{
    public class OrderSaga : ISaga, InitiatedBy<OrderCreated>, Orchestrates<NotificationSent>, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public OrderState OrderState { get; set; } = new OrderState();
        public int Version { get; set; }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            OrderState.CustomerId = context.Message.CustomerId;
            OrderState.CreatedAt = context.Message.CreatedAt;
            OrderState.UpdatedAt = DateTime.UtcNow;
            
            OrderState.CurrentState = nameof(OrderCreated);

            await context.Publish(new NotificationSent(
                context.Message.OrderId,
                $"Order {context.Message.OrderId} was created by user {context.Message.CustomerId}"));
        }

        public Task Consume(ConsumeContext<NotificationSent> context)
        {
            OrderState.CurrentState = nameof(NotificationSent);
            OrderState.NotificationSentAt = DateTime.UtcNow;

            return Task.CompletedTask;
        }
    }
}

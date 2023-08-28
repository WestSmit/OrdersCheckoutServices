using MassTransit;
using OrdersCheckoutServices.Notifications.Models;
using OrdersCheckoutServices.Shared.Order.Contracts;

namespace OrdersCheckoutServices.Notifications.Consumers
{
    public class OrderCreatedConsumer : IConsumer<NotificationRequested>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly IBus _bus;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<NotificationRequested> context)
        {
            var notification = new Notification()
            {
                OrderId = context.Message.OrderId,
                Message = "Order has been created",
                CustomerId = context.Message.CustomerId,
            };

            // Notification service call

            _logger?.LogInformation("Sending notifications...");

            var isSuccess = true; // Emulating the result of notification service call

            if (isSuccess)
            {
                await _bus.Publish(new NotificationSent() { OrderId = notification.OrderId });
            }
            else
            {
                await _bus.Publish(new NotificationSendingFailed() { OrderId = notification.OrderId });
            }
            
        }
    }
}

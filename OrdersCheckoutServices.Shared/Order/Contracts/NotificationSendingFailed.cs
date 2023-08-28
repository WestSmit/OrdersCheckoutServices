using MassTransit;

namespace OrdersCheckoutServices.Shared.Order.Contracts
{
    public class NotificationSendingFailed
    {
        public Guid OrderId { get; set; }
    }
}

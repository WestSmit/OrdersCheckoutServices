using MassTransit;

namespace OrdersCheckoutServices.Shared.Order.Contracts
{
    public class NotificationSent
    {
        public Guid OrderId { get; set; }
    }
}

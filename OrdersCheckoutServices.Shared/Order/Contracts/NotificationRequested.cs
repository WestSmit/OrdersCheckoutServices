namespace OrdersCheckoutServices.Shared.Order.Contracts
{
    public class NotificationRequested
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}

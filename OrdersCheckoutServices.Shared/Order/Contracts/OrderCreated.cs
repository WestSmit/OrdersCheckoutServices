using MassTransit;

namespace OrdersCheckoutServices.Shared.Order.Contracts
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
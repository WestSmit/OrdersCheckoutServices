namespace OrdersCheckoutServices.Notifications.Models
{
    public class Notification
    {
        public Guid OrderId { get; set; }
        public string Message { get; set; }
        public Guid CustomerId { get; set; }
    }
}

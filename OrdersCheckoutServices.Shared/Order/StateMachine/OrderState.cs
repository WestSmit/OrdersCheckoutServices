using MassTransit;

namespace OrdersCheckoutServices.Shared.Order.StateMachine
{
    public class OrderState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? NotificationSentAt { get; set; }
        public string? Reason { get; set; }
        public int Version { get; set; }
    }
}

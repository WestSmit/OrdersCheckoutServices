using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Models;
using OrdersCheckoutServices.Shared.Order.Contracts;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IBus _bus;

        public OrderController(ILogger<OrderController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderRequest orderRequest)
        {
            var order = new Models.Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(orderRequest.UserId),
                CreatedAt = DateTime.UtcNow,
                Products = orderRequest.Products,
            };

            // Order creating...

            var orderCreated = new OrderCreated()
            {
                OrderId = order.Id,
                CustomerId = order.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _bus.Publish(orderCreated);

            return CreatedAtAction(nameof(Create), order);
        }
    }
}
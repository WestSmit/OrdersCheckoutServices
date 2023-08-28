using MassTransit;
using OrdersCheckoutServices.Notifications.Configuration;
using OrdersCheckoutServices.Notifications.Consumers;
using OrdersCheckoutServices.Shared.Order.StateMachine;

var builder = WebApplication.CreateBuilder(args);

// Configurations
var rabbitMqConfig = builder.Configuration.GetRequiredSection("RabbitMq").Get<RabbitMqConfiguration>();
var mongoDbConfig = builder.Configuration.GetRequiredSection("MongoDb").Get<MongoDbConfiguration>();

builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();

    x.AddConsumer<OrderCreatedConsumer>()
        .Endpoint(cfg => cfg.Name = "order_saga");

    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .Endpoint(cfg => cfg.Name = "order_saga")
        .MongoDbRepository(r =>
        {
            r.Connection = mongoDbConfig.Connection;
            r.DatabaseName = mongoDbConfig.DatabaseName;
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqConfig.Host, h =>
        {
            h.Username(rabbitMqConfig.UserName);
            h.Password(rabbitMqConfig.Password);
        });

        cfg.UseMessageRetry(c => c.Interval(5, TimeSpan.FromSeconds(2)));
        cfg.UseInMemoryOutbox();
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();
app.Run();

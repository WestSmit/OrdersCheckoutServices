using MassTransit;
using MassTransit.Configuration;
using OrdersCheckoutServices.Api.Orders.Configuration;

var builder = WebApplication.CreateBuilder(args);
var rabbitMqConfig = builder.Configuration.GetRequiredSection("RabbitMq").Get<RabbitMqConfiguration>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqConfig.Host, h =>
        {
            h.Username(rabbitMqConfig.UserName);
            h.Password(rabbitMqConfig.Password);
        });
        cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

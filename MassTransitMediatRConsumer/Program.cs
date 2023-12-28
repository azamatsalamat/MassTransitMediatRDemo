using System.Reflection;
using MassTransit;
using MassTransitMediatRConsumer.Consumers;
using MassTransitMediatRConsumer.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appName = Assembly.GetExecutingAssembly().GetName().Name;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqOptions>(options => 
    builder.Configuration.GetSection("RabbitMQ").Bind(options));
builder.Services.AddSingleton(x => x.GetService<IOptions<RabbitMqOptions>>().Value);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderFromFacebookReceivedConsumer>();
    x.AddConsumer<OrderFromInstagramReceivedConsumer>();
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = context.GetRequiredService<RabbitMqOptions>();
        var rabbitHost = new Uri(new Uri($"rabbitmq://{rabbit.MainHost}"), rabbit.VirtualHost);
        cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromMinutes(1)));
        cfg.Host(rabbitHost, appName, h =>
        {
            h.Username(rabbit.UserName);
            h.Password(rabbit.Password);
            h.UseCluster(c => Array.ForEach(rabbit.HostNames, c.Node));
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

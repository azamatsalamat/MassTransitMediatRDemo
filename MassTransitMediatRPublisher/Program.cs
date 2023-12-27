using MassTransit;
using MassTransitMediatRPublisher.Options;
using MassTransitMediatRPublisher.Pipeline;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var appName = Assembly.GetExecutingAssembly().GetName().Name;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region RabbitMQ & MassTransit
builder.Services.Configure<RabbitMqOptions>(options =>
    builder.Configuration.GetSection("RabbitMQ").Bind(options));
builder.Services.AddSingleton(x => x.GetService<IOptions<RabbitMqOptions>>().Value);
builder.Services.AddMassTransit(x =>
{
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
#endregion

#region MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
});
#endregion

var app = builder.Build();

var busControl = app.Services.GetService<IBusControl>();
app.Lifetime.ApplicationStarted.Register(busControl.Start);
app.Lifetime.ApplicationStopped.Register(busControl.Stop);

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

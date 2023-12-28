using MassTransit;
using MassTransitMediatRConsumer.Commands;
using MassTransitMediatRContracts;
using MediatR;

namespace MassTransitMediatRConsumer.Consumers;

public class OrderFromInstagramReceivedConsumer : IConsumer<OrderFromInstagramReceived>
{
    private readonly IMediator _mediator;
    public OrderFromInstagramReceivedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderFromInstagramReceived> context)
    {
        await Console.Out.WriteLineAsync("Received an order from Instagram");
        var request = new NewInstagramOrderCommand(context.Message.MessageText);
        await _mediator.Send(request);
    }
}

using MassTransit;
using MassTransitMediatRConsumer.Commands;
using MassTransitMediatRContracts;
using MediatR;

namespace MassTransitMediatRConsumer.Consumers;

public class OrderFromFacebookReceivedConsumer : IConsumer<OrderFromFacebookReceived>
{
    private readonly IMediator _mediator;
    public OrderFromFacebookReceivedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderFromFacebookReceived> context)
    {
        await Console.Out.WriteLineAsync("Received an order from Facebook");
        var request = new NewFacebookOrderCommand(context.Message.MessageText);
        await _mediator.Send(request);
    }
}

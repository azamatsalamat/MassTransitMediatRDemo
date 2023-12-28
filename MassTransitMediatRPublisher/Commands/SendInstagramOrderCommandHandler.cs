using MassTransit;
using MassTransitMediatRContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Commands;

public class SendInstagramOrderCommandHandler : IRequestHandler<SendInstagramOrderCommand, ActionResult>
{
    private readonly IBus _publishEndpoint;
    public SendInstagramOrderCommandHandler(IBus publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ActionResult> Handle(SendInstagramOrderCommand request, CancellationToken cancellationToken)
    {
        var response = $"Executed a request from Instagram with a message: {request.MessageText}";
        await _publishEndpoint.Publish(new OrderFromInstagramReceived
        {
            MessageText = request.MessageText
        });
        return new OkObjectResult(response);
    }
}

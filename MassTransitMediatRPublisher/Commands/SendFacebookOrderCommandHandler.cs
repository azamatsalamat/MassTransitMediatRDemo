using MassTransit;
using MassTransitMediatRContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Commands;

public class SendFacebookOrderCommandHandler : IRequestHandler<SendFacebookOrderCommand, ActionResult>
{
    private readonly IBus _publishEndpoint;
    public SendFacebookOrderCommandHandler(IBus publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ActionResult> Handle(SendFacebookOrderCommand request, CancellationToken cancellationToken)
    {
        var response = $"Executed a request from Facebook with a message: {request.MessageText}";
        await _publishEndpoint.Publish(new OrderFromFacebookReceived
        {
            MessageText = request.MessageText,
        });
        return new OkObjectResult(response);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Commands;

public class SendInstagramOrderCommandHandler : IRequestHandler<SendInstagramOrderCommand, ActionResult>
{
    public async Task<ActionResult> Handle(SendInstagramOrderCommand request, CancellationToken cancellationToken)
    {
        var response = $"Executed a request from Instagram with a message: {request.MessageText}";
        return new OkObjectResult(response);
    }
}

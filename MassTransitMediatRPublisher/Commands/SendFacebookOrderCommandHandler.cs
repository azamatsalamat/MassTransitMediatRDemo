using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Commands;

public class SendFacebookOrderCommandHandler : IRequestHandler<SendFacebookOrderCommand, ActionResult>
{
    public async Task<ActionResult> Handle(SendFacebookOrderCommand request, CancellationToken cancellationToken)
    {
        var response = $"Executed a request from Facebook with a message: {request.MessageText}";
        return new OkObjectResult(response);
    }
}

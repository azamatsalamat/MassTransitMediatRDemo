using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRConsumer.Commands;

public class NewFacebookOrderCommandHandler : IRequestHandler<NewFacebookOrderCommand, ActionResult>
{
    public async Task<ActionResult> Handle(NewFacebookOrderCommand request, CancellationToken cancellationToken)
    {
        var response = $"Processed a new order from Facebook: {request.MessageText}";
        await Console.Out.WriteLineAsync(response);
        return new OkObjectResult(response);
    }
}

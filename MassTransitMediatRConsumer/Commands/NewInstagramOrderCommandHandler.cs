using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRConsumer.Commands;

public class NewInstagramOrderCommandHandler : IRequestHandler<NewInstagramOrderCommand, ActionResult>
{
    public async Task<ActionResult> Handle(NewInstagramOrderCommand request, CancellationToken cancellationToken)
    {
        var response = $"Processed an order from Instagram: {request.MessageText}";
        await Console.Out.WriteLineAsync(response);
        return new OkObjectResult(response);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Pipeline;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : ActionResult
{
    private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;
    public ValidationPipelineBehavior()
    {

    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"Starting the request {typeof(TRequest).Name}");
        var response = await next();
        await Console.Out.WriteLineAsync($"Finished the request {typeof(TRequest).Name}");
        return response;
    }
}
 
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Abstractions;

public interface IMessageRequest<TResponse> : IRequest<TResponse> where TResponse : ActionResult
{
}

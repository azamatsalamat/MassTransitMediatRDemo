using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Commands;

public class SendInstagramOrderCommand : IRequest<ActionResult>
{
    public string MessageText { get; set; }
    public SendInstagramOrderCommand(string text)
    {
        MessageText = text;
    }
}

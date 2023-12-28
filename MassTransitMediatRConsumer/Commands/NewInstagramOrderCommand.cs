using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRConsumer.Commands;

public class NewInstagramOrderCommand : IRequest<ActionResult>
{
    public string MessageText { get; set; }
    public NewInstagramOrderCommand(string messageText)
    {
        MessageText = messageText;
    }
}

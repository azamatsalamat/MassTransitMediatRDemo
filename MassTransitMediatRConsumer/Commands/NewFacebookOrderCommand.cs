using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRConsumer.Commands;

public class NewFacebookOrderCommand : IRequest<ActionResult>
{
    public string MessageText { get; set; }
    public NewFacebookOrderCommand(string messageText)
    {
        MessageText = messageText;
    }
}

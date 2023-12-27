using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Commands;

public class SendFacebookOrderCommand : IRequest<ActionResult>
{
    public string MessageText { get; set; }
    public SendFacebookOrderCommand(string text)
    {
        MessageText = text;
    }
}

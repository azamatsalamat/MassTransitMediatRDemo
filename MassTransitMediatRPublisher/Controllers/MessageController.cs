using MassTransitMediatRPublisher.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMediatRPublisher.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{ 
    private IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("insta")]
    public async Task<IActionResult> SendInstagramOrder([FromQuery] string text)
    {
        var handlerRequest = new SendInstagramOrderCommand(text);
        var handlerResponse = await _mediator.Send(handlerRequest);
        return handlerResponse;
    }

    [HttpPost("facebook")]
    public async Task<IActionResult> SendFacebookOrder([FromQuery] string text)
    {
        var handlerRequest = new SendFacebookOrderCommand(text);
        var handlerResponse = await _mediator.Send(handlerRequest);
        return handlerResponse;
    }
}
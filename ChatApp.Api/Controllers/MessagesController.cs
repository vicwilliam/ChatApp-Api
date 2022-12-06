using ChatApp.Api.Controllers.Base;
using ChatApp.Api.Hubs;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : BaseController<Message>
{
    private readonly IMessageService _messageService;
    private readonly IHubContext<WebSocketHub> _wsHubContext;

    public MessagesController(IMessageService baseService,
        IHttpContextAccessor httpContextAccessor,
        IHubContext<WebSocketHub> wsHubContext) : base(baseService, httpContextAccessor)
    {
        _messageService = baseService;
        _wsHubContext = wsHubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(SendMessageDto dto)
    {
        dto.AuthorId = Guid.Parse("01ee577b-7c6f-443b-815c-3129ce4509e6");
        await _messageService.SendMessage(dto);
        await _wsHubContext.Clients.All.SendAsync("newMessage", dto);
        Console.WriteLine("sent");
        return Ok();
    }

    [HttpPost("sendCommand")]
    public async Task<IActionResult> SendCommand(SendMessageDto dto)
    {
        return null;
    }

    [HttpGet("last50")]
    public async Task<IActionResult> GetMessages([FromQuery] Guid roomId)
    {
        var result = await _messageService.GetTop50(roomId);
        return Ok(result);
    }
}
using ChatApp.Api.Controllers.Base;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Hubs;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : Controller
{
    private readonly IMessageService _messageService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public MessagesController(IMessageService baseService,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _messageService = baseService;
        this.httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(SendMessageDto dto)
    {
        var currentuser = httpContextAccessor.HttpContext.User;
        await _messageService.SendMessage(dto, currentuser);
        return Ok();
    }


    [HttpPost("sendCommand")]
    [AllowAnonymous]
    public async Task<IActionResult> SendCommand(SendCommandDto dto)
    {
        await Task.Yield();
        _messageService.SendCommand(dto);
        return Ok();
    }

    [HttpGet("last50")]
    public async Task<IActionResult> GetMessages([FromQuery] Guid roomId)
    {
        var result = await _messageService.GetTop50(roomId);
        return Ok(result);
    }
}
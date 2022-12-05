using ChatApp.Api.Controllers.Base;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : BaseController<Message>
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService baseService,
        IHttpContextAccessor httpContextAccessor) : base(baseService, httpContextAccessor)
    {
        _messageService = baseService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(SendMessageDto dto)
    {
        // dto.AuthorId = UserId;
        dto.AuthorId = Guid.Parse("01ee577b-7c6f-443b-815c-3129ce4509e6");
        await _messageService.SendMessage(dto);
        return Ok();
    }
}
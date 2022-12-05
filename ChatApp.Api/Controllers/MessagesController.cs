using ChatApp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController
{
    public MessagesController()
    {
        
    }
    [HttpPost("send")]
    public IActionResult SendMessage(SendMessageDto dto)
    {
        return ;
    }
}
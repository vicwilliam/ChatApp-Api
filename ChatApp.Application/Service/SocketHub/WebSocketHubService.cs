using ChatApp.Application.Service.Hubs;
using ChatApp.Application.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Application.Service.SocketHub;

public class WebSocketHubService : IWebSocketHubService
{
    private readonly IHubContext<WebSocketHub> _hubContext;

    public WebSocketHubService(IHubContext<WebSocketHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(Guid roomId)
    {
        await _hubContext.Clients.All
            .SendAsync("newMessage", new object?[] { roomId.ToString() });
    }
}
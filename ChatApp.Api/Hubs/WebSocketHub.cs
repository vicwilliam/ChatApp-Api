using ChatApp.Application.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs;

public class WebSocketHub : Hub
{
    public async Task SendMessage(Guid roomId)
    {
        await Clients.All
            .SendCoreAsync("newMessage", new object?[] { roomId.ToString() });
    }
}
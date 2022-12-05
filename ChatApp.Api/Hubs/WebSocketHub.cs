using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs;

public class WebSocketHub : Hub
{
    public async Task SendMessage(string email, string message)
    {
        await Clients.Group(email)
            .SendCoreAsync("newMessage", new[] { message });
    }
}
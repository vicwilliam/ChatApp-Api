namespace ChatApp.Application.Service.Interfaces;

public interface IWebSocketHubService
{
    Task SendMessage(Guid roomId);
}
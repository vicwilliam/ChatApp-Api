using System.Security.Claims;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Service.Interfaces;

public interface IMessageService : IBaseService<Message>
{
    Task SendMessage(SendMessageDto dto, ClaimsPrincipal? userClaims);
    Task<List<MessageReturnDto>> GetTop50(Guid roomId);

    void SendCommand(SendCommandDto dto);
}
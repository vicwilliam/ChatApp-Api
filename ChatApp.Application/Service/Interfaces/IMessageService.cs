using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Service.Interfaces;

public interface IMessageService : IBaseService<Message>
{
    Task SendMessage(SendMessageDto dto);
}
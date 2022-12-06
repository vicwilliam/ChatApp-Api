using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Service.Entities;

public class MessageService : BaseService<Message>, IMessageService
{
    public MessageService(AppDbContext context) : base(context)
    {
    }

    public async Task SendMessage(SendMessageDto dto)
    {
        var message = new Message
        {
            AuthorId = dto.AuthorId,
            RoomId = dto.RoomId,
            Content = dto.Content
        };
        await Insert(message);
    }

    public async Task<ICollection<Message>> GetTop50(Guid roomId)
    {
        var result = await DbContext.Set<Message>().Where(x => x.RoomId == roomId)
            .OrderByDescending(x => x.CreatedAt).Take(50).OrderBy(x=>x.CreatedAt).ToListAsync();
        return result;
    }
}
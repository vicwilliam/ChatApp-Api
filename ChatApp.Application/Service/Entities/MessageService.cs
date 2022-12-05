using ChatApp.Application.Service.Base;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;

namespace ChatApp.Application.Service.Entities;

public class MessageService : BaseService<Message>, IBaseService<Message>
{
    public MessageService(AppDbContext context) : base(context)
    {
    }
}
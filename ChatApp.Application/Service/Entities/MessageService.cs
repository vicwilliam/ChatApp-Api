using System.Security.Claims;
using System.Text;
using ChatApp.Application.Dtos;
using ChatApp.Application.RMQ;
using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChatApp.Application.Service.Entities;

public class MessageService : BaseService<Message>, IMessageService
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly UserManager<User> _userManager;

    public MessageService(AppDbContext context,
        IRabbitMqService rabbitMqService,
        UserManager<User> userManager) :
        base(context)
    {
        _rabbitMqService = rabbitMqService;
        _userManager = userManager;
    }

    public async Task SendMessage(SendMessageDto dto, ClaimsPrincipal? userClaims)
    {
        if (userClaims != null)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            dto.AuthorId = user.Id;
        }

        var message = new Message
        {
            AuthorId = dto.AuthorId,
            RoomId = dto.RoomId,
            Content = dto.Content
        };

        await Insert(message);
    }

    public async Task<List<MessageReturnDto>> GetTop50(Guid roomId)
    {
        var result = await DbContext.Set<Message>()
            .Where(x => x.RoomId == roomId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(50)
            .OrderBy(x => x.CreatedAt)
            .Include(x => x.Author)
            .Select(x => new MessageReturnDto()
            {
                Content = x.Content,
                AuthorId = x.AuthorId,
                AuthorUserName = x.Author.UserName
            })
            .ToListAsync();
        return result;
    }

    public void SendCommand(SendCommandDto dto)
    {
        var supportedCommands = new string[] { "stock" };
        if (!supportedCommands.Contains(dto.Command))
        {
            throw new InvalidOperationException("Command is not supported");
        }

        using var connection = _rabbitMqService.CreateChannel();
        using var model = connection.CreateModel();
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
        model.BasicPublish("StocksExchange", "", true, null, body: body);
    }
}
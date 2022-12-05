using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;

namespace ChatApp.Application.Service.Entities;

public class RoomService : BaseService<Room>, IRoomService
{
    public RoomService(AppDbContext context) : base(context)
    {
    }

    public async Task CreateRoom(CreateRoomDto dto)
    {
        try
        {
            var entity = await Insert(new Room()
            {
                Name = dto.Name,
                CreatorId = dto.CreatorId
            });
            //TODO: Implement signaling
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
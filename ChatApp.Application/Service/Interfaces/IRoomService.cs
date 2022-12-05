using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Domain.Models;

namespace ChatApp.Application.Service.Interfaces;

public interface IRoomService : IBaseService<Room>
{
    public Task CreateRoom(CreateRoomDto dto);
}
using ChatApp.Api.Controllers.Base;
using ChatApp.Api.Hubs;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : BaseController<Room>
{
    private readonly IRoomService _roomService;
    private readonly IHubContext<WebSocketHub> _wsHubContext;

    public RoomController(IRoomService baseService,
        IHttpContextAccessor httpContextAncestor,
        IHubContext<WebSocketHub> wsHubContext)
        : base(baseService, httpContextAncestor)
    {
        _roomService = baseService;
        _wsHubContext = wsHubContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto dto)
    {
        dto.CreatorId = Guid.Parse("01ee577b-7c6f-443b-815c-3129ce4509e6");
        await _roomService.CreateRoom(dto);
        await _wsHubContext.Clients.All.SendAsync("newRoom");
        return Created("/", "");
    }

    [HttpGet("list")]
    public IActionResult ListRooms()
    {
        return Ok(_roomService.GetAll());
    }
}
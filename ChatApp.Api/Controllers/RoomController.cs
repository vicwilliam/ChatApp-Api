using ChatApp.Api.Controllers.Base;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Hubs;
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
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHubContext<WebSocketHub> _wsHubContext;

    public RoomController(IRoomService baseService,
        IHttpContextAccessor httpContextAncestor,
        IHttpContextAccessor httpContextAccessor,
        IHubContext<WebSocketHub> wsHubContext)
        : base(baseService, httpContextAncestor)
    {
        _roomService = baseService;
        _httpContextAccessor = httpContextAccessor;
        _wsHubContext = wsHubContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto dto)
    {
        var currentuser = _httpContextAccessor.HttpContext.User;

        await _roomService.CreateRoom(dto, currentuser);
        await _wsHubContext.Clients.All.SendAsync("newRoom");
        return Created("/", "");
    }

    [HttpGet("list")]
    public IActionResult ListRooms()
    {
        return Ok(_roomService.GetAll());
    }
}
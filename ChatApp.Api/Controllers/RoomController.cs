using ChatApp.Api.Controllers.Base;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : BaseController<Room>
{
    private readonly IRoomService roomService;

    public RoomController(IRoomService baseService,
        IHttpContextAccessor httpContextAncestor)
        : base(baseService, httpContextAncestor)
    {
        roomService = baseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto dto)
    {
        dto.CreatorId = Guid.Parse("01ee577b-7c6f-443b-815c-3129ce4509e6");
        await roomService.CreateRoom(dto);
        return Ok();
    }

    [HttpGet("list")]
    public IActionResult ListRooms()
    {
        return Ok(roomService.GetAll());
    }
}
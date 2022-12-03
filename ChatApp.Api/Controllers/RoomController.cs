using ChatApp.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

public class RoomController : Controller
{
    [HttpPost("create")]
    public IActionResult Index(RoomCreateDto dto)
    {
        return null;
    }
}
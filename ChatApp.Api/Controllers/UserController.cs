using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService service;

    public UserController(IUserService service)
    {
        this.service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterLoginDto dto)
    {
        var result = await service.RegisterUser(dto);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(UserRegisterLoginDto dto)
    {
        var result = await service.LoginUser(dto);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
}
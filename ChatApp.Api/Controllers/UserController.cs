using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class UserController
{
    [HttpPost("register")]
    public IActionResult Register()
    {
        return null;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate()
    {
        return null;
    }
}
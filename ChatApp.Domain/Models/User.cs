using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Domain.Models;

public class User : IdentityUser<Guid>
{
    public bool IsBot { get; set; } = false;
}
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Domain.Models;

public class User : IdentityUser<Guid>
{
    public User(string username)
    {
        Username = username;
    }

    public string Username { get; set; }
    public virtual ICollection<UserInRooms> JoinedRooms { get; set; }
}
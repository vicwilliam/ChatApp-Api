namespace ChatApp.Domain.Models;

public class UserInRooms
{
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public Room Room { get; set; }
    public User User { get; set; }
    public Guid IdRoom { get; set; }
    public Guid IdUser { get; set; }
}
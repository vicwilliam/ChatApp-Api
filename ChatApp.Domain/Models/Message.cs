namespace ChatApp.Domain.Models;

public class Message : BaseEntity
{
    public string Content { get; set; }
    public User Author { get; set; }
    public Room Room { get; set; }
    
    public Guid AuthorId { get; set; }
    public Guid RoomId { get; set; }
}
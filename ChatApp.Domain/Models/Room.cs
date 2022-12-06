namespace ChatApp.Domain.Models;

public class Room : BaseEntity
{
    public string Name { get; set; }
    public User Creator { get; set; }
    public Guid CreatorId { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}
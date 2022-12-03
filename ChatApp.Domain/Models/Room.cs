namespace ChatApp.Domain.Models;

public class Room : BaseEntity
{
    public string Name { get; set; }
    public List<User> Members { get; set; }
    public User Creator { get; set; }
    public List<Message> Messages { get; set; }
}
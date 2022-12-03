namespace ChatApp.Domain.Models;

public class User : BaseEntity
{
    public User(string username)
    {
        Username = username;
    }

    public string Username { get; set; }
}
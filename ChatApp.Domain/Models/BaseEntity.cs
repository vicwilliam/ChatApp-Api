namespace ChatApp.Domain.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }= new Guid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public BaseEntity()
    {
    }
}
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Domain.Models;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; set; } = new Guid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public BaseEntity()
    {
    }
}
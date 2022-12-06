namespace ChatApp.Application.Dtos;

public class SendMessageDto
{
    public string Content { get; set; }
    public Guid RoomId { get; set; }
    public Guid AuthorId { get; set; }
}

public class SendCommandDto
{
    public string Command { get; set; }
    public string Parameter { get; set; }
    public Guid RoomId { get; set; }
}
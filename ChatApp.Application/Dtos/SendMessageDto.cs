namespace ChatApp.Application.Dtos;

public class SendMessageDto
{
    public string Message { get; set; }
    public Guid RoomId { get; set; }
}
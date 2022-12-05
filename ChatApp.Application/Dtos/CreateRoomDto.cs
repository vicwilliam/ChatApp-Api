namespace ChatApp.Application.Dtos;

public class CreateRoomDto
{
    public string Name { get; set; }
    public Guid CreatorId { get; set; }
}
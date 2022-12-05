namespace ChatApp.Application.Dtos;

public class CreateRoomDto
{
    public string Name { get; set; }
    public Guid CreatorId { get; set; }
}

public class AddUserToRoomDto
{
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
}
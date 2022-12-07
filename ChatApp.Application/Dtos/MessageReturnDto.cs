namespace ChatApp.Application.Dtos;

public class MessageReturnDto
{
    public string Content { get; set; }
    public DateTime TimeStamp { get; set; }
    public string AuthorUserName { get; set; }
    public Guid AuthorId { get; set; }
}
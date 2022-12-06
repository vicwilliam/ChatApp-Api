namespace ChatApp.Application.Dtos;

public class JwtTokenDto
{
    public DateTime CreatedAt { get; set; }
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
}
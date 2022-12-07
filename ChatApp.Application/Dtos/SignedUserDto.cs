namespace ChatApp.Application.Dtos;

public class SignedUserDto
{
    public string UserName { get; set; }
    public JwtTokenDto Token { get; set; }
}
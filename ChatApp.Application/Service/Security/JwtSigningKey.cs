using System.Text;
using ChatApp.Application.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Application.Service.Security;

public class JwtSigningKey 
{
    public SigningCredentials SigningCredentials { get; set; }

    public JwtSigningKey(JwtTokenOptions jwtTokenOptions)
    {
        var keyBytes = Encoding.UTF8.GetBytes(jwtTokenOptions.SecurityKey);
        var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);

        SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}
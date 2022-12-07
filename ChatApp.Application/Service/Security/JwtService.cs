using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatApp.Application.Dtos;
using ChatApp.Application.Options;
using ChatApp.Application.Service.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Application.Service.Security;

public class JwtService : IJwtService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly JwtTokenOptions _jwtTokenOptions;
    private readonly JwtSigningKey _jwtSigningKey;

    public JwtService(
        IOptions<JwtTokenOptions> jwtTokenOptions,
        JwtSigningKey jwtSigningKey)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        _jwtTokenOptions = jwtTokenOptions.Value;
        _jwtSigningKey = jwtSigningKey;
    }

    public JwtTokenDto GenerateToken(ClaimsIdentity userIdentity)
    {
        DateTime authTime = DateTime.UtcNow;
        DateTime expirationTime = authTime.AddSeconds(_jwtTokenOptions.ExpirationTime);

        userIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")));
        userIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.AuthTime, authTime.ToString("u")));

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = userIdentity,
            NotBefore = authTime,
            Expires = expirationTime,
            IssuedAt = authTime,
            Audience = _jwtTokenOptions.Audience,
            Issuer = _jwtTokenOptions.Issuer,
            SigningCredentials = _jwtSigningKey.SigningCredentials,
        };

        SecurityToken securityToken = _tokenHandler.CreateToken(tokenDescriptor);
        return new JwtTokenDto()
        {
            Token = _tokenHandler.WriteToken(securityToken),
            CreatedAt = authTime,
            ExpiresIn = _jwtTokenOptions.ExpirationTime,
        };
    }
}
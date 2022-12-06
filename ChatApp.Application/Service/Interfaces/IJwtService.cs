using System.Security.Claims;
using ChatApp.Application.Dtos;

namespace ChatApp.Application.Service.Interfaces;

public interface IJwtService
{
    JwtTokenDto GenerateToken(ClaimsIdentity userIdentity);
}
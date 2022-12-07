using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Application.Service.Interfaces;

public interface IUserService
{
    Task<Guid> GetUserIdFromUsername(string username);
    Task<IdentityResult> RegisterUser(UserRegisterLoginDto dto);
    Task<SignedUserDto> LoginUser(UserRegisterLoginDto dto);
}
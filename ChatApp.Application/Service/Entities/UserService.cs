using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatApp.Application.Service.Entities;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Guid> GetUserIdFromUsername(string username)
    {
        var result = await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        if (result == null)
            throw new Exception("User not found");
        return result.Id;
    }

    public async Task<IdentityResult> RegisterUser(UserRegisterLoginDto dto)
    {
        var user = new User() { UserName = dto.Username };
        var result = await _userManager.CreateAsync(user, dto.Password);
        return result;
    }

    public async Task<SignInResult> LoginUser(UserRegisterLoginDto dto)
    {
        var user = await _userManager.Users.Where(x => x.UserName == dto.Username).FirstOrDefaultAsync();
        if (user == null)
            throw new Exception("User not found");
        var signinResult = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);
        return signinResult;
    }
}
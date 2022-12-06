using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatApp.Application.Service.Entities;

public class UserService : IUserService
{
    protected UserService(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
    {
    }
}
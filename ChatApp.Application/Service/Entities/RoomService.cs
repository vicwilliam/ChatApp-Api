using System.Security.Claims;
using ChatApp.Application.Dtos;
using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Application.Service.Entities;

public class RoomService : BaseService<Room>, IRoomService
{
    private readonly UserManager<User> _userManager;

    public RoomService(AppDbContext context,
        UserManager<User> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task CreateRoom(CreateRoomDto dto, ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal != null)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            dto.CreatorId = user.Id;
        }

        try
        {
            var entity = await Insert(new Room()
            {
                Name = dto.Name,
                CreatorId = dto.CreatorId
            });
            //TODO: Implement signaling
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
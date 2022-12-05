using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Entities;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using ChatApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application.DependencyInjection;

public class Initializer
{
    public static void Configure(IServiceCollection services, string connection)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection));

        services.AddScoped<IRepository<User>,UserRepository>();
        services.AddScoped<IRepository<User>,UserRepository>();
        
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<IMessageService, MessageService>();
        
    }
}
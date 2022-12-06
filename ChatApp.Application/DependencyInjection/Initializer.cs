using ChatApp.Application.RMQ;
using ChatApp.Application.Service.Base;
using ChatApp.Application.Service.Entities;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using ChatApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application.DependencyInjection;

public class Initializer
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<User>, UserRepository>();

        services.Configure<RabbitMqConfiguration>(a =>
            configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IConsumerService, ConsumerService>();
        services.AddHostedService<ConsumerHostedService>();


        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<IMessageService, MessageService>();
    }
}
using ChatApp.Application.RMQ;
using ChatApp.Application.Options;
using ChatApp.Application.Service.Entities;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Application.Service.Security;
using ChatApp.Application.Service.SocketHub;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Application.DependencyInjection;

public static class Initializer
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        JwtTokenOptions tokenOptions = configuration.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>();
        JwtSigningKey jwtSigningKey = new JwtSigningKey(tokenOptions);

        services.AddDbContext<AppDbContext>(
            opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<User, IdentityRole<Guid>>(u =>
        {
            u.SignIn.RequireConfirmedAccount = false;
            u.SignIn.RequireConfirmedEmail = false;
            u.SignIn.RequireConfirmedPhoneNumber = false;
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        services.Configure<RabbitMqConfiguration>(a =>
            configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IConsumerService, ConsumerService>();

        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWebSocketHubService, WebSocketHubService>();

        services.AddHostedService<ConsumerHostedService>();
        services.Configure<JwtTokenOptions>(a => configuration.GetSection(nameof(JwtTokenOptions)).Bind(a));
        services.AddSingleton(jwtSigningKey);
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = tokenOptions.Audience,
                ValidIssuer = tokenOptions.Issuer,
                ValidateAudience = true,
                ValidateIssuer = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = jwtSigningKey.SigningCredentials.Key,
            };
        });
    }
}
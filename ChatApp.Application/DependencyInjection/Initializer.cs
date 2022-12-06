using ChatApp.Application.RMQ;
using ChatApp.Application.Options;
using ChatApp.Application.Service.Entities;
using ChatApp.Application.Service.Interfaces;
using ChatApp.Application.Service.Security;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Application.DependencyInjection;

public class Initializer
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
        }).AddEntityFrameworkStores<AppDbContext>();

        services.Configure<RabbitMqConfiguration>(a =>
            configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IConsumerService, ConsumerService>();
        services.AddHostedService<ConsumerHostedService>();

        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddScoped<IJwtService, JwtService>();

        services.Configure<JwtTokenOptions>(a => configuration.GetSection(nameof(JwtTokenOptions)).Bind(a));
        services.AddSingleton(jwtSigningKey);
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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
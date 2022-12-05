using ChatApp.Api.Hubs;
using ChatApp.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Initializer.Configure(builder.Services,
    builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

var app = builder.Build();
app.UseWebSockets();
app.MapHub<WebSocketHub>("/ws");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(cors =>
{
    cors.WithOrigins("http://localhost:3000");
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
    cors.AllowCredentials();
});
app.Run();
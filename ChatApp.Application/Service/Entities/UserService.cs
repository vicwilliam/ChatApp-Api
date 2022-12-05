using ChatApp.Application.Service.Base;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;

namespace ChatApp.Application.Service.Entities;

public class UserService : BaseService<User>, IBaseService<User>
{
    protected UserService(AppDbContext context) : base(context)
    {
    }
}
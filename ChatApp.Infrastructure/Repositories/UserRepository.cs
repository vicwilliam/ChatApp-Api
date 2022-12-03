using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;

namespace ChatApp.Infrastructure.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
using Microsoft.EntityFrameworkCore;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Repositories;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(ServiceManagerContext context) : base(context, context.Users)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await Context.Users.Where(p => p.Username == username).SingleOrDefaultAsync();
        return user;
    }
}
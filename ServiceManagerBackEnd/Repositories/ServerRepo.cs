using Microsoft.EntityFrameworkCore;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Repositories;

public class ServerRepo : BaseRepo<Server>, IServerRepo
{
    private readonly ServiceManagerContext _context;

    public ServerRepo(ServiceManagerContext context) : base(context, context.Servers)
    {
        _context = context;
    }

    public async Task<List<Server>> GetUserServerAsync(int userId)
    {
        var servers = _context.Servers.Where(p => p.UserId == userId);
        return await servers.ToListAsync();
    }
}
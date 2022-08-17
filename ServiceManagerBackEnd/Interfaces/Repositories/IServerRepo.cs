using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Interfaces.Repositories;

public interface IServerRepo : IBaseRepo<Server>
{
    public Task<List<Server>> GetUserServerAsync(int userId);
}
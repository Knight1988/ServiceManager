using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Interfaces.Repositories;

public interface IUserRepo : IBaseRepo<User>
{
    Task<User?> GetByUsernameAsync(string username);
}
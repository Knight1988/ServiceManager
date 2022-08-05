using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Services;

public class UserService : BaseService<User>, IUserService
{
    public UserService(IUserRepo baseRepo) : base(baseRepo)
    {
    }
}
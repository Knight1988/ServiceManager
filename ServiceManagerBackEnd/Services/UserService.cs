using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Services;

public class UserService : BaseService<User>, IUserService
{
    public UserService(IUserRepo baseRepo) : base(baseRepo)
    {
    }

    public override Task AddAsync(User model)
    {
        // encrypt password
        model.Password = Helper.EncryptPassword(model.Username, model.Password);
        return base.AddAsync(model);
    }
}
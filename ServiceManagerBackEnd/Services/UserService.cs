using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo) : base(userRepo)
    {
        _userRepo = userRepo;
    }

    public override async Task AddAsync(User model)
    {
        // check user exist
        var user = await _userRepo.GetByUsernameAsync(model.Username);
        // encrypt password
        model.Password = Helper.EncryptPassword(model.Username, model.Password);
        // add user
        await base.AddAsync(model);
    }
}
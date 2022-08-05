using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;

namespace ServiceManagerBackEnd.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepo _userRepo;

    public AuthenticationService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    
    public async Task<LoginResult> LoginAsync(string username, string password)
    {
        try
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null)
            {
                return LoginResult.UserAndPasswordNotMatch;
            }

            var encryptedPassword = Helper.EncryptPassword(username, password);
            if (user.Password != encryptedPassword)
            {
                return LoginResult.UserAndPasswordNotMatch;
            }

            return LoginResult.Success;
        }
        catch (Exception e)
        {
            return LoginResult.Exception;
        }
    }
}
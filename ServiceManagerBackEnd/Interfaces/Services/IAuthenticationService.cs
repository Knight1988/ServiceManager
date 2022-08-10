using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Interfaces.Services;

public interface IAuthenticationService
{
    Task<(LoginResult Success, LoginResponse response)> LoginAsync(string username, string password);
}
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Interfaces.Services;

public interface IAuthenticationService
{
    Task<(int errorCode, LoginResponse? response)> LoginAsync(string username, string password);
}
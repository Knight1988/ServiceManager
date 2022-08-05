namespace ServiceManagerBackEnd.Interfaces.Services;

public interface IAuthenticationService
{
    Task<LoginResult> LoginAsync(string username, string password);
}
namespace ServiceManagerBackEnd.Interfaces.Services;

public interface IAuthenticationService
{
    Task<(LoginResult Success, string? token)> LoginAsync(string username, string password);
}
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Interfaces.Services;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(string username, string password);
    Task<LoginResponse> RefreshTokenAsync(int id);
}
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}
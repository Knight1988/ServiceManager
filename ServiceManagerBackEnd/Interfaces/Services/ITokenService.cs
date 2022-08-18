using System.IdentityModel.Tokens.Jwt;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user);
    void ValidateToken(string token);
    int GetUserId(string token);
}
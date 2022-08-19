using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Exceptions;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeService _dateTimeService;

    public TokenService(IConfiguration configuration, IDateTimeService dateTimeService)
    {
        _configuration = configuration;
        _dateTimeService = dateTimeService;
    }
    
    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: _dateTimeService.TokenExpireDate(),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public void ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();
        SecurityToken securityToken;
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out securityToken);
        }
        catch (Exception e)
        {
            throw new TokenInvalidException(e);
        }
        if (securityToken.ValidTo < DateTime.Now)
        {
            throw new TokenExpiredException();
        }
    }

    public JwtSecurityToken Decode(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        return jwtSecurityToken;
    }

    public int GetUserId(string token)
    {
        ValidateToken(token);
        var jwtSecurityToken = Decode(token);
        var claim = jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "id");
        if (claim == null) throw new GeneralException(ErrorCodes.TokenInvalid, "Id not found");
        var userId = Convert.ToInt32(claim.Value);
        return userId;
    }

    private TokenValidationParameters GetValidationParameters()
    {
        var secret = _configuration.GetValue<string>("Jwt:Secret");
        var key = Encoding.ASCII.GetBytes(secret);
        return new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false, // Because there is no audiance in the generated token
            ValidateIssuer = false,   // Because there is no issuer in the generated token
            IssuerSigningKey = new SymmetricSecurityKey(key) // The same key as the one that generate the token
        };
    }
}
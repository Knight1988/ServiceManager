using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepo _userRepo;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(ILogger<AuthenticationService> logger, ITokenService tokenService, IUserRepo userRepo)
    {
        _logger = logger;
        _tokenService = tokenService;
        _userRepo = userRepo;
    }
    
    public async Task<(LoginResult Success, LoginResponse response)> LoginAsync(string username, string password)
    {
        try
        {
            _logger.LogInformation("Start check login user {Username}", username);
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogInformation("User {Username} not found", username);
                return (LoginResult.UserAndPasswordNotMatch, null);
            }

            var encryptedPassword = Helper.EncryptPassword(username, password);
            if (user.Password != encryptedPassword)
            {
                _logger.LogInformation("Password not match");
                return (LoginResult.UserAndPasswordNotMatch, null);
            }

            _logger.LogInformation("User {Username} login success", username);
            var token = _tokenService.GenerateJwtToken(user);

            var response = new LoginResponse()
            {
                Username = user.Username,
                Name = user.Name,
                Token = token
            };
            return (LoginResult.Success, response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "User {Username} login failed", username);
            return (LoginResult.Exception, null);
        }
    }
}
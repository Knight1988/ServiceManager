using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Exceptions;
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
    
    public async Task<LoginResponse> LoginAsync(string username, string password)
    {
        _logger.LogInformation("Start check login user {Username}", username);
        var user = await _userRepo.GetByUsernameAsync(username);
        if (user == null)
        {
            _logger.LogInformation("User {Username} not found", username);
            throw new GeneralException(ErrorCodes.UserAndPasswordNotMatch, $"User {username} not found");
        }

        var encryptedPassword = Helper.EncryptPassword(username, password);
        if (user.Password != encryptedPassword)
        {
            _logger.LogInformation("Password not match");
            throw new GeneralException(ErrorCodes.UserAndPasswordNotMatch, "Password not match");
        }

        _logger.LogInformation("User {Username} login success", username);
        var token = _tokenService.GenerateJwtToken(user);

        var response = new LoginResponse()
        {
            Username = user.Username,
            Name = user.Name,
            Token = token
        };
        return response;
    }

    public async Task<LoginResponse> RefreshTokenAsync(int id)
    {
        var user = await _userRepo.GetAsync(id);
        if (user == null) throw new UserNotFoundException();
        
        var token = _tokenService.GenerateJwtToken(user);
        var response = new LoginResponse()
        {
            Username = user.Username,
            Name = user.Name,
            Token = token
        };
        return response;
    }
}
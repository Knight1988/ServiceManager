using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;

    public AuthenticationController(IAuthenticationService authenticationService, ITokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var (errorCode, loginResponse) = await _authenticationService.LoginAsync(request.Username, request.Password);
        return errorCode switch
        {
            ErrorCodes.UserAndPasswordNotMatch => ActionResultFactory.UnprocessableEntity(ErrorCodes.UserAndPasswordNotMatch,
                "User and password does not match"),
            ErrorCodes.None => ActionResultFactory.Ok("Login Success", loginResponse),
            ErrorCodes.InternalServerError => ActionResultFactory.InternalServerError("There was error on server"),
            _ => ActionResultFactory.NotImplemented("This response is not implemented")
        };
    }

    [HttpPost("verify_token")]
    public async Task<IActionResult> VerifyTokenAsync([FromBody] VerifyTokenRequest request)
    {
        var token = request.Token;
        var result = _tokenService.ValidateToken(token);
        return result switch
        {
            true => ActionResultFactory.Ok("Token valid", token),
            _ => ActionResultFactory.UnprocessableEntity(ErrorCodes.TokenInvalid, "Token invalid")
        };
    }
}
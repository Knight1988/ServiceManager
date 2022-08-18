using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Exceptions;
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
        try
        {
            var loginResponse = await _authenticationService.LoginAsync(request.Username, request.Password);
            return ActionResultFactory.Ok("Login Success", loginResponse);
        }
        catch (GeneralException e)
        {
            return e.ErrorCode switch
            {
                ErrorCodes.UserAndPasswordNotMatch => ActionResultFactory.UnprocessableEntity(ErrorCodes.UserAndPasswordNotMatch,
                    "User and password does not match"),
                _ => ActionResultFactory.InternalServerError("There was error on server")
            };
        }
        catch (Exception e)
        {
            return ActionResultFactory.InternalServerError("There was error on server");
        }
    }

    [HttpPost("verify_token")]
    public async Task<IActionResult> VerifyTokenAsync([FromBody] VerifyTokenRequest request)
    {
        try
        {
            var id = _tokenService.GetUserId(request.Token);
            var loginResponse = await _authenticationService.RefreshTokenAsync(id);
            return ActionResultFactory.Ok("Verify token success", loginResponse);
        }
        catch (GeneralException e)
        {
            return ActionResultFactory.UnprocessableEntity(e.ErrorCode, e.Message);
        }
        catch (Exception e)
        {
            return ActionResultFactory.InternalServerError();
        }
    }
}
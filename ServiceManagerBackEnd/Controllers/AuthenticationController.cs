using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : CustomBaseController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var (response, token) = await _authenticationService.LoginAsync(request.Username, request.Password);
        switch (response)
        {
            case LoginResult.UserAndPasswordNotMatch:
                return UnprocessableEntity(ErrorCodes.UserAndPasswordNotMatch, "User and password does not match");
            case LoginResult.Success:
                return Ok("Login Success", token);
            case LoginResult.Exception:
                return InternalServerError("There was error on server");
            default:
                return NotImplemented("This response is not implemented");
        }
    }
}
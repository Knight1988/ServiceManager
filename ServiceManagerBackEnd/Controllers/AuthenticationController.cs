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
        var (errorCode, token) = await _authenticationService.LoginAsync(request.Username, request.Password);
        return errorCode switch
        {
            ErrorCodes.UserAndPasswordNotMatch => UnprocessableEntity(ErrorCodes.UserAndPasswordNotMatch,
                "User and password does not match"),
            ErrorCodes.Success => Ok("Login Success", token),
            ErrorCodes.InternalServerError => InternalServerError("There was error on server"),
            _ => NotImplemented("This response is not implemented")
        };
    }
}
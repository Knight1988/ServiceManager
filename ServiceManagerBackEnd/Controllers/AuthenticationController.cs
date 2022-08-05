using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost("login")]
    public async Task<BaseResponse> LoginAsync(LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request.Username, request.Password);
        switch (response)
        {
            case LoginResult.UserAndPasswordNotMatch:
                return ResponseFactory.Create((int) response, "User and password does not match");
            case LoginResult.Success:
                return ResponseFactory.Success("Login Success");
            default:
                return ResponseFactory.Exception("This response is not implemented");
        }
    }
}
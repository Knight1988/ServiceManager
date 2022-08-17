using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Exceptions;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest userRequest)
    {
        try
        {
            _logger.LogInformation("Create user {Username}", userRequest.Username);
            var user = new User()
            {
                Username = userRequest.Username,
                Name = userRequest.Name,
                Password = userRequest.Password
            };
            await _userService.AddAsync(user);
            return ActionResultFactory.Created("User created");
        }
        catch (GeneralException e)
        {
            _logger.LogInformation("Failed to create user, code: {Code}", e.ErrorCode);
            switch (e.ErrorCode)
            {
                case ErrorCodes.DataExist:
                    return ActionResultFactory.UnprocessableEntity(e.ErrorCode, "User exists");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when create user");
            return ActionResultFactory.InternalServerError("Error when create user");
        }
        
        _logger.LogError("This ");
        return ActionResultFactory.InternalServerError("Error when create user");
    }
}
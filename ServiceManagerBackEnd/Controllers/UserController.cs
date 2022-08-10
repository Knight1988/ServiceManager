using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task Create([FromBody] CreateUserRequest userRequest)
    {
        var user = new User()
        {
            Username = userRequest.Username,
            Name = userRequest.Name,
            Password = userRequest.Password
        };
        await _userService.AddAsync(user);
    }
}
using Moq;
using NUnit.Framework;
using ServiceManagerBackEnd.Controllers;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Tests.Controllers;

[TestFixture]
public class UserControllerTests
{
    [Test]
    public async Task TestCreateUser_Success()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
        var user = new CreateUserRequest()
        {
            Name = "Test",
            Password = "Test",
            Username = "Test"
        };

        await userController.CreateAsync(user);
    }
}
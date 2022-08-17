using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Controllers;
using ServiceManagerBackEnd.Exceptions;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Models.Requests;
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Tests.Controllers;

[TestFixture]
public class UserControllerTests
{
    [Test]
    public async Task TestCreateUser_Success()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(NullLogger<UserController>.Instance, userServiceMock.Object);
        var user = new CreateUserRequest()
        {
            Name = "Test",
            Password = "Test",
            Username = "Test"
        };

        var result = await userController.CreateAsync(user);
        var value = result.GetValue<BaseResponse>();
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.None);
    }
    
    [Test]
    public async Task TestCreateUser_UserExist_ShouldReturnUserExist()
    {
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(s => s.AddAsync(It.IsAny<User>())).ThrowsAsync(new GeneralException(ErrorCodes.DataExist, "User exists"));
        var userController = new UserController(NullLogger<UserController>.Instance, userServiceMock.Object);
        var user = new CreateUserRequest()
        {
            Name = "Test",
            Password = "Test",
            Username = "Test"
        };

        var result = await userController.CreateAsync(user);
        var value = result.GetValue<BaseResponse>();
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.DataExist);
    }
}
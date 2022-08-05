using FluentAssertions;
using Moq;
using ServiceManagerBackEnd.Controllers;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models.Requests;

namespace ServiceManagerBackEnd.Tests.Controllers;

[TestFixture]
public class AuthenticationControllerTests
{
    [Test]
    public async Task TestLogin_Success_ShouldReturnSuccess()
    {
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync(LoginResult.Success);
        var controller = new AuthenticationController(authenticationServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        response.ErrorCode.Should().Be(0);
    }
    
    [Test]
    public async Task TestLogin_Failed_ShouldReturnNotMatch()
    {
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync(LoginResult.UserAndPasswordNotMatch);
        var controller = new AuthenticationController(authenticationServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        response.ErrorCode.Should().Be(1);
    }
    
    [Test]
    public async Task TestLogin_Exception_ShouldReturnException()
    {
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync(LoginResult.Exception);
        var controller = new AuthenticationController(authenticationServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        response.ErrorCode.Should().Be(500);
    }
}
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Controllers;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models.Requests;
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Tests.Controllers;

[TestFixture]
public class AuthenticationControllerTests
{
    [Test]
    public async Task TestLogin_Success_ShouldReturnSuccess()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync((Success: ErrorCodes.None, new LoginResponse
        {
            Name = "Test",
            Username = "Test",
            Token = "Token"
        }));
        var controller = new AuthenticationController(authenticationServiceMock.Object, tokenServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        var okResult = response as ObjectResult;
        var value = okResult.Value as BaseResponse<LoginResponse>;
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(0);
        value.Data.Token.Should().Be("Token");
    }
    
    [Test]
    public async Task TestLogin_Failed_ShouldReturnNotMatch()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync((ErrorCodes.UserAndPasswordNotMatch, null));
        var controller = new AuthenticationController(authenticationServiceMock.Object, tokenServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        var okResult = response as ObjectResult;
        var value = okResult.Value as BaseResponse;
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.UserAndPasswordNotMatch);
    }
    
    [Test]
    public async Task TestLogin_Exception_ShouldReturnException()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync((ErrorCodes.InternalServerError, null));
        var controller = new AuthenticationController(authenticationServiceMock.Object, tokenServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        var objectResult = response as ObjectResult;
        var value = objectResult.Value as BaseResponse;
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(500);
    }

    [Test]
    public async Task Token_Valid_ShouldReturnErrorCodeNone()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(s => s.ValidateToken(It.IsAny<string>())).Returns(true);
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        var controller = new AuthenticationController(authenticationServiceMock.Object, tokenServiceMock.Object);

        var response = await controller.VerifyTokenAsync(new VerifyTokenRequest());
        var value = response.GetValue<BaseResponse>();

        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.None);
    }

    [Test]
    public async Task Token_Invalid_ShouldReturnTokenInvalid()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(s => s.ValidateToken(It.IsAny<string>())).Returns(false);
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        var controller = new AuthenticationController(authenticationServiceMock.Object, tokenServiceMock.Object);

        var response = await controller.VerifyTokenAsync(new VerifyTokenRequest());
        var value = response.GetValue<BaseResponse>();

        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.TokenInvalid);
    }
}
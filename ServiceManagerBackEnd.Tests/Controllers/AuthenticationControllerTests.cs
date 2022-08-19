using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Controllers;
using ServiceManagerBackEnd.Exceptions;
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
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test")).ReturnsAsync(new LoginResponse
        {
            Name = "Test",
            Username = "Test",
            Token = "Token"
        });
        var controller = new AuthenticationController(NullLogger<AuthenticationController>.Instance, authenticationServiceMock.Object, tokenServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        var value = response.GetValue<BaseResponse<LoginResponse>>();
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(0);
        value.Data.Token.Should().Be("Token");
    }
    
    [Test]
    public async Task TestLogin_Failed_ShouldReturnNotMatch()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test"))
            .ThrowsAsync(new GeneralException(ErrorCodes.UserAndPasswordNotMatch, "Password not match"));
        var controller = new AuthenticationController(NullLogger<AuthenticationController>.Instance, authenticationServiceMock.Object, tokenServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        var value = response.GetValue<BaseResponse>();
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.UserAndPasswordNotMatch);
    }
    
    [Test]
    public async Task TestLogin_Exception_ShouldReturnException()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.LoginAsync("Test", "Test"))
            .ThrowsAsync(new Exception( "This is exception"));
        var controller = new AuthenticationController(NullLogger<AuthenticationController>.Instance, authenticationServiceMock.Object, tokenServiceMock.Object);
        var request = new LoginRequest
        {
            Username = "Test",
            Password = "Test"
        };

        var response = await controller.LoginAsync(request);
        var value = response.GetValue<BaseResponse>();
        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(500);
    }

    [Test]
    public async Task Token_Valid_ShouldReturnErrorCodeNone()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(s => s.ValidateToken(It.IsAny<string>()));
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        var controller = new AuthenticationController(NullLogger<AuthenticationController>.Instance, authenticationServiceMock.Object, tokenServiceMock.Object);

        var response = await controller.VerifyTokenAsync(new VerifyTokenRequest());
        var value = response.GetValue<BaseResponse>();

        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.None);
    }

    [Test]
    public async Task Token_Invalid_ShouldReturnTokenInvalid()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(s => s.RefreshTokenAsync(It.IsAny<int>()))
            .ThrowsAsync(new TokenInvalidException());
        var controller = new AuthenticationController(NullLogger<AuthenticationController>.Instance, authenticationServiceMock.Object, tokenServiceMock.Object);

        var response = await controller.VerifyTokenAsync(new VerifyTokenRequest());
        var value = response.GetValue<BaseResponse>();

        value.Should().NotBeNull();
        value.ErrorCode.Should().Be(ErrorCodes.TokenInvalid);
    }
}
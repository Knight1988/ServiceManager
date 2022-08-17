using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServiceManagerBackEnd.Commons;
using ServiceManagerBackEnd.Exceptions;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Services;

namespace ServiceManagerBackEnd.Tests.Services;

public class AuthenticationServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestLogin_Match_ShouldReturnSuccess()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(s => s.GenerateJwtToken(It.IsAny<User>())).Returns("Token");
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new User
        {
            Name = "Test",
            Password = "2c2e30ff38cb4046fdfeb4be97844c49f2c0cd85e345f2653914a202e5bde244"
        });
        var service = new AuthenticationService(NullLogger<AuthenticationService>.Instance, tokenServiceMock.Object,
            userRepoMock.Object);

        var response = await service.LoginAsync("Test", "Test");

        response.Name.Should().Be("Test");
        response.Token.Should().Be("Token");
    }

    [Test]
    public async Task TestLogin_NotFound_ShouldThrowGeneralException()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
        var service = new AuthenticationService(NullLogger<AuthenticationService>.Instance, tokenServiceMock.Object,
            userRepoMock.Object);

        var act = async () => { await service.LoginAsync("Test", "Test"); };

        await act.Should().ThrowAsync<GeneralException>();
    }

    [Test]
    public async Task TestLogin_NotMatch_ShouldThrowGeneralException()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new User
        {
            Name = "Test",
            Password = "asd"
        });
        var service = new AuthenticationService(NullLogger<AuthenticationService>.Instance, tokenServiceMock.Object,
            userRepoMock.Object);
        
        var act = async () => { await service.LoginAsync("Test", "Test"); };

        await act.Should().ThrowAsync<GeneralException>();
    }

    [Test]
    public async Task TestLogin_Exception_ShouldThrowException()
    {
        var tokenServiceMock = new Mock<ITokenService>();
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).Throws(new Exception("Test"));
        var service = new AuthenticationService(NullLogger<AuthenticationService>.Instance, tokenServiceMock.Object,
            userRepoMock.Object);

        var act = async () => { await service.LoginAsync("Test", "Test"); };

        await act.Should().ThrowAsync<Exception>();
    }
}
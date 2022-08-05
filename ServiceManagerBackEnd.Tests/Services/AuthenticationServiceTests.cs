using FluentAssertions;
using Moq;
using ServiceManagerBackEnd.Interfaces.Repositories;
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
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new User
        {
            Name = "Test",
            Password = "2c2e30ff38cb4046fdfeb4be97844c49f2c0cd85e345f2653914a202e5bde244"
        });
        var service = new AuthenticationService(userRepoMock.Object);

        var result = await service.LoginAsync("Test", "Test");

        result.Should().Be(LoginResult.Success);
    }

    [Test]
    public async Task TestLogin_NotFound_ShouldReturnNotMatch()
    {
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
        var service = new AuthenticationService(userRepoMock.Object);

        var result = await service.LoginAsync("Test", "Test");

        result.Should().Be(LoginResult.UserAndPasswordNotMatch);
    }

    [Test]
    public async Task TestLogin_NotMatch_ShouldReturnNotMatch()
    {
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new User
        {
            Name = "Test",
            Password = "asd"
        });
        var service = new AuthenticationService(userRepoMock.Object);

        var result = await service.LoginAsync("Test", "Test");

        result.Should().Be(LoginResult.UserAndPasswordNotMatch);
    }

    [Test]
    public async Task TestLogin_Exception_ShouldReturnException()
    {
        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(p => p.GetByUsernameAsync(It.IsAny<string>())).Throws(new Exception("Test"));
        var service = new AuthenticationService(userRepoMock.Object);

        var result = await service.LoginAsync("Test", "Test");

        result.Should().Be(LoginResult.Exception);
    }
}
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Services;

namespace ServiceManagerBackEnd.Tests.Services;

[TestFixture]
public class TokenServiceTests
{
    private IConfiguration _config;

    [SetUp]
    public void SetUp()
    {
        // init config
        _config = Helper.InitConfiguration();
    }
    
    [Test]
    public void Token_Valid_ShouldReturnTrue()
    {
        var dateTimeServiceMock = new Mock<IDateTimeService>();
        dateTimeServiceMock.Setup(s => s.TokenExpireDate()).Returns(DateTime.Now.AddDays(7));
        var tokenService = new TokenService(_config, dateTimeServiceMock.Object);

        var token = tokenService.GenerateJwtToken(new User()
        {
            Id = 1
        });
        var isValid = tokenService.ValidateToken(token);

        isValid.Should().BeTrue();
    }
    
    [Test]
    public void Token_Invalid_ShouldReturnFalse()
    {
        var dateTimeServiceMock = new Mock<IDateTimeService>();
        var tokenService = new TokenService(_config, dateTimeServiceMock.Object);
        
        var isValid = tokenService.ValidateToken("asd");

        isValid.Should().BeFalse();
    }
    
    [Test]
    public async Task Token_Expired_ShouldReturnFalse()
    {
        var dateTimeServiceMock = new Mock<IDateTimeService>();
        dateTimeServiceMock.Setup(s => s.TokenExpireDate()).Returns(DateTime.UtcNow.AddSeconds(1));
        var tokenService = new TokenService(_config, dateTimeServiceMock.Object);
        
        var token = tokenService.GenerateJwtToken(new User()
        {
            Id = 1
        });
        await Task.Delay(1000);
        var isValid = tokenService.ValidateToken(token);

        isValid.Should().BeFalse();
    }
}
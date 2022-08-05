using FluentAssertions;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Repositories;

namespace ServiceManagerBackEnd.Tests.Repositories;

[TestFixture]
public class UserRepoTests
{
    private ServiceManagerContext _context;

    [SetUp]
    public async Task SetUp()
    {
        // init database
        _context = Helper.GetDataContext();
        await _context.Database.EnsureCreatedAsync();
        
        // Seed data
        await _context.Users.AddAsync(new User()
        {
            Name = "Test",
            Username = "Test",
            Password = "2c2e30ff38cb4046fdfeb4be97844c49f2c0cd85e345f2653914a202e5bde244"
        });
        await _context.SaveChangesAsync();
    }

    [Test]
    public async Task TestGetByUsername_Test_ShouldReturnObject()
    {
        var repo = new UserRepo(_context);

        var user = await repo.GetByUsernameAsync("Test");

        user.Name.Should().Be("Test");
    }

    [Test]
    public async Task TestGetByUsername_NotExist_ShouldReturnObject()
    {
        var repo = new UserRepo(_context);

        var user = await repo.GetByUsernameAsync("NotExist");

        user.Should().BeNull();
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await _context.Database.EnsureDeletedAsync();
    }
}
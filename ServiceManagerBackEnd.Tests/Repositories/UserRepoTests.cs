using FluentAssertions;
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
    }

    [Test]
    public async Task TestGetByUsername_Test_ShouldReturnObject()
    {
        var repo = new UserRepo(_context);

        var user = await repo.GetByUsernameAsync("admin@demo.com");

        user.Name.Should().Be("Demo");
        user.Password.Should().Be(Commons.Helper.EncryptPassword("admin@demo.com", "demo"));
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
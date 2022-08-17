using FluentAssertions;
using NUnit.Framework;
using ServiceManagerBackEnd.Repositories;

namespace ServiceManagerBackEnd.Tests.Repositories;

[TestFixture]
public class ServerRepoTests
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
    public async Task GetUserServerList_ShouldReturnNotDeletedServer()
    {
        var serverRepo = new ServerRepo(_context);

        var server = await serverRepo.GetUserServerAsync(1);

        server.Count.Should().Be(1);
    }
}